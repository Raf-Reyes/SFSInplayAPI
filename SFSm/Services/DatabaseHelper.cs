using Microsoft.Data.SqlClient;
using SFSm.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SFSm.Services
{
    public class DatabaseHelper
    {
        private static SQLiteConnection _database;
        public static void InitializeDatabase()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "LocalDB.db");
            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<UserModel>();
            _database.CreateTable<SO1Model>();
            _database.CreateTable<SO2Model>();
            _database.CreateTable<SOPickModel>();
            _database.CreateTable<SOPick2Model>();
            //DropSODatabase();
        }

        public void DropUserTable()
        {
            _database.DropTable<UserModel>();
            _database.CreateTable<UserModel>();
        }

        public static void DropSODatabase()
        {
            _database.DropTable<SO1Model>();
            _database.DropTable<SO2Model>();
            _database.DropTable<SOPickModel>();
            _database.DropTable<SOPick2Model>();
            _database.CreateTable<SO1Model>();
            _database.CreateTable<SO2Model>();
            _database.CreateTable<SOPickModel>();
            _database.CreateTable<SOPick2Model>();
        }

        public static SQLiteConnection GetConnection() => _database;

        public void InsertUpdateUsers(List<UserModel> users)
        {
            foreach (var user in users)
            {
                var existingUser = _database.Find<UserModel>(u => u.EmpID == user.EmpID);
                if (existingUser == null)
                {
                    _database.Insert(user);
                }
                else
                {
                    existingUser.Pass = user.Pass;
                    existingUser.EmpID = user.EmpID;
                    _database.Update(existingUser);
                }
            }
        }

        public void InsertSO1Data(List<SO1Model> so1Data)
        {
            if (so1Data != null && so1Data.Any())
            {
                _database.InsertAll(so1Data);
            }
        }

        public static void InsertSOPick1(SOPickModel refsono)
        {
            _database.Insert(refsono);
        }

        public static void DeleteSOPick1(int tseqno)
        {
            _database.Execute("DELETE FROM SOPickModel WHERE Refsono = ?", tseqno);
        }

        public static void InsertSOPick2(List<SOPick2Model> pick2)
        {
            _database.InsertAll(pick2);
        }

        public static void DeleteSOPick2(int tseqno)
        {
            _database.Execute("DELETE FROM SOPick2Model WHERE Refsono = ?", tseqno);
        }

        public void InsertSO2Data(List<SO2Model> so2Data)
        {
            if (so2Data != null && so2Data.Any())
            {
                _database.InsertAll(so2Data);
            }
        }

        public void DeleteSO(int tseqno, bool validate = true)
        {
            if (validate)
            {
                //_database.Execute("DELETE FROM SO1Model WHERE Tseqno = ?", tseqno); //para makapag download ulit ng same SO
                _database.Execute("UPDATE SO1Model SET Active = 0 WHERE Tseqno = ?", tseqno); //para 1 SO exist lang per phone database
                _database.Execute("DELETE FROM SO2Model WHERE Tseqno = ?", tseqno);
            }
            _database.Execute("DELETE FROM SOPickModel WHERE Refsono = ?", tseqno);
            _database.Execute("DELETE FROM SOPick2Model WHERE Refsono = ?", tseqno);
        }

        public static void DeleteBarcode(int tseqno, string barcode)
        {
            _database.Execute("DELETE FROM SO2Model WHERE Tseqno = ? AND barcode = ?", tseqno, barcode);
            _database.Execute("DELETE FROM SOPick2Model WHERE Refsono = ? AND barcode = ?", tseqno, barcode);
        }

        public void UpdateQTYSO2Model(int tseqno, string desc, decimal count, string serialNo, string barcode)
        {
            // Delete from SOPick2Model
            desc = desc.Trim();
            _database.Execute("DELETE FROM SOPick2Model WHERE Refsono = ? AND Description = ?", tseqno, desc);

            // Update SO2Model Qty
            _database.Execute("UPDATE SO2Model SET Qty = Qty - ? WHERE Tseqno = ? AND Desc = ?", count, tseqno, desc);

            //  Check if Qty is now 0 and delete if 0
            int remainingQty = _database.ExecuteScalar<int>("SELECT Qty FROM SO2Model WHERE Tseqno = ? AND Desc = ?", tseqno, desc);
            if (remainingQty == 0)
            {
                _database.Execute("DELETE FROM SO2Model WHERE Tseqno = ? AND Desc = ?", tseqno, desc);
            }

            // Remove saved preferences
            string key = $"Serials_{tseqno}_{barcode.Trim()}";
            Preferences.Remove(key);

            // Check if SOPick2Model has any remaining records in SOPickModel
            int sopickCount = _database.ExecuteScalar<int>("SELECT COUNT(*) FROM SOPick2Model WHERE Refsono = ?", tseqno);
            if (sopickCount == 0)
            {
                _database.Execute("DELETE FROM SOPickModel WHERE Refsono = ?", tseqno);
            }
        }

        public static List<SO1Model> ShowSO1()
        {
            try
            {
                var so1Data = _database.Table<SO1Model>().ToList();

                return so1Data;
            }
            catch (Exception ex)
            {

                return new List<SO1Model>();
            }
        }

        public static List<SO2Model> ShowSO2(int tseqno)
        {
            try
            {
                var so2Data = _database.Table<SO2Model>().Where(x => x.Tseqno == tseqno).ToList();
                return so2Data;
            }
            catch(Exception ex)
            {
                return new List<SO2Model>();
            }
        }

        public static List<SO2Model> ShowSO2()
        {
            try
            {
                var so2Data = _database.Table<SO2Model>().ToList();

                var uniqueTSQ = so2Data.GroupBy(x => x.Tseqno).Select(grp => grp.First()).OrderBy(x => x.Tseqno).ToList();
                return uniqueTSQ;
            }
            catch (Exception ex)
            {
                return new List<SO2Model>();
            }
        }

        public List<SOPickModel> ShowPick1()
        {
            try
            {
                var pick1 = _database.Table<SOPickModel>().ToList();

                return pick1;
            }
            catch (Exception ex)
            {

                return new List<SOPickModel>();
            }
        }

        public List<SOPick2Model> ShowPick2(int refsono)
        {
            try
            {
                var pick2Data = _database.Table<SOPick2Model>().Where(x => x.Refsono == refsono).ToList();
                return pick2Data;
            }
            catch (Exception ex)
            {
                return new List<SOPick2Model>();
            }
        }

        public bool Authentication(string username, string password)
        {
            var user = _database.Find<UserModel>(u => u.Name == username);
            if (user == null) return false;

            var encrptedPassword = EncryptThisText(password.ToUpper());
            return user.Pass == encrptedPassword;
        }

        public string EncryptThisText(string txt)
        {
            string encryptedText = string.Empty;

            foreach (char p in txt)
            {
                switch (p)
                {
                    case '1': encryptedText += 'b'; break;
                    case '2': encryptedText += 'd'; break;
                    case '3': encryptedText += 'f'; break;
                    case '4': encryptedText += 'h'; break;
                    case '5': encryptedText += 'j'; break;
                    case '6': encryptedText += 'l'; break;
                    case '7': encryptedText += 'n'; break;
                    case '8': encryptedText += 'p'; break;
                    case '9': encryptedText += 'r'; break;
                    case '0': encryptedText += '`'; break;
                    case 'Q': encryptedText += 'â'; break;
                    case 'W': encryptedText += 'î'; break;
                    case 'E': encryptedText += 'Ê'; break;
                    case 'R': encryptedText += 'ä'; break;
                    case 'T': encryptedText += 'è'; break;
                    case 'Y': encryptedText += 'ò'; break;
                    case 'U': encryptedText += 'ê'; break;
                    case 'I': encryptedText += 'Ò'; break;
                    case 'O': encryptedText += 'Þ'; break;
                    case 'P': encryptedText += 'à'; break;
                    case 'A': encryptedText += 'Â'; break;
                    case 'S': encryptedText += 'æ'; break;
                    case 'D': encryptedText += 'È'; break;
                    case 'F': encryptedText += 'Ì'; break;
                    case 'G': encryptedText += 'Î'; break;
                    case 'H': encryptedText += 'Ð'; break;
                    case 'J': encryptedText += 'Ô'; break;
                    case 'K': encryptedText += 'Ö'; break;
                    case 'L': encryptedText += 'Ø'; break;
                    case 'Z': encryptedText += 'ô'; break;
                    case 'X': encryptedText += 'ð'; break;
                    case 'C': encryptedText += 'Æ'; break;
                    case 'V': encryptedText += 'ì'; break;
                    case 'B': encryptedText += 'Ä'; break;
                    case 'N': encryptedText += 'Ü'; break;
                    case 'M': encryptedText += 'Ú'; break;
                    default: encryptedText += p; break;
                }
            }

            return encryptedText;
        }

        public string GetEmpID(string username)
        {
            var user = _database.Table<UserModel>().FirstOrDefault(u => u.Name == username);
            return user?.EmpID;
        }
    }


}
