; ModuleID = 'marshal_methods.armeabi-v7a.ll'
source_filename = "marshal_methods.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [215 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [430 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 160
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 159
	i32 34715100, ; 2: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 120
	i32 39109920, ; 3: Newtonsoft.Json.dll => 0x254c520 => 77
	i32 40744412, ; 4: Xamarin.AndroidX.Camera.Lifecycle.dll => 0x26db5dc => 95
	i32 42639949, ; 5: System.Threading.Thread => 0x28aa24d => 201
	i32 57725457, ; 6: it\Microsoft.Data.SqlClient.resources => 0x370d211 => 4
	i32 57727992, ; 7: ja\Microsoft.Data.SqlClient.resources => 0x370dbf8 => 5
	i32 66541672, ; 8: System.Diagnostics.StackTrace => 0x3f75868 => 143
	i32 67008169, ; 9: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 46
	i32 68219467, ; 10: System.Security.Cryptography.Primitives => 0x410f24b => 190
	i32 72070932, ; 11: Microsoft.Maui.Graphics.dll => 0x44bb714 => 75
	i32 117431740, ; 12: System.Runtime.InteropServices => 0x6ffddbc => 178
	i32 122350210, ; 13: System.Threading.Channels.dll => 0x74aea82 => 199
	i32 139659294, ; 14: ja/Microsoft.Data.SqlClient.resources.dll => 0x853081e => 5
	i32 142721839, ; 15: System.Net.WebHeaderCollection => 0x881c32f => 167
	i32 149972175, ; 16: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 190
	i32 159306688, ; 17: System.ComponentModel.Annotations => 0x97ed3c0 => 135
	i32 165246403, ; 18: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 98
	i32 166535111, ; 19: ru/Microsoft.Data.SqlClient.resources.dll => 0x9ed1fc7 => 9
	i32 182336117, ; 20: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 116
	i32 195452805, ; 21: vi/Microsoft.Maui.Controls.resources.dll => 0xba65f85 => 43
	i32 199333315, ; 22: zh-HK/Microsoft.Maui.Controls.resources.dll => 0xbe195c3 => 44
	i32 205061960, ; 23: System.ComponentModel => 0xc38ff48 => 138
	i32 209399409, ; 24: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 93
	i32 230752869, ; 25: Microsoft.CSharp.dll => 0xdc10265 => 127
	i32 246610117, ; 26: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 175
	i32 264223668, ; 27: zh-Hans\Microsoft.Data.SqlClient.resources => 0xfbfbbb4 => 11
	i32 280992041, ; 28: cs/Microsoft.Maui.Controls.resources.dll => 0x10bf9929 => 15
	i32 317674968, ; 29: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 43
	i32 318968648, ; 30: Xamarin.AndroidX.Activity.dll => 0x13031348 => 90
	i32 330147069, ; 31: Microsoft.SqlServer.Server => 0x13ada4fd => 76
	i32 336156722, ; 32: ja/Microsoft.Maui.Controls.resources.dll => 0x14095832 => 28
	i32 342366114, ; 33: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 105
	i32 347068432, ; 34: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0x14afd810 => 81
	i32 356389973, ; 35: it/Microsoft.Maui.Controls.resources.dll => 0x153e1455 => 27
	i32 360671332, ; 36: pl\Microsoft.Data.SqlClient.resources => 0x157f6864 => 7
	i32 367780167, ; 37: System.IO.Pipes => 0x15ebe147 => 154
	i32 374914964, ; 38: System.Transactions.Local => 0x1658bf94 => 204
	i32 375677976, ; 39: System.Net.ServicePoint.dll => 0x16646418 => 164
	i32 379916513, ; 40: System.Threading.Thread.dll => 0x16a510e1 => 201
	i32 385762202, ; 41: System.Memory.dll => 0x16fe439a => 157
	i32 392610295, ; 42: System.Threading.ThreadPool.dll => 0x1766c1f7 => 202
	i32 395744057, ; 43: _Microsoft.Android.Resource.Designer => 0x17969339 => 47
	i32 407321033, ; 44: tr/Microsoft.Data.SqlClient.resources.dll => 0x184739c9 => 10
	i32 435591531, ; 45: sv/Microsoft.Maui.Controls.resources.dll => 0x19f6996b => 39
	i32 442565967, ; 46: System.Collections => 0x1a61054f => 134
	i32 450948140, ; 47: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 104
	i32 451504562, ; 48: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 191
	i32 459347974, ; 49: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 183
	i32 469710990, ; 50: System.dll => 0x1bff388e => 209
	i32 485463106, ; 51: Microsoft.IdentityModel.Abstractions => 0x1cef9442 => 65
	i32 498788369, ; 52: System.ObjectModel => 0x1dbae811 => 169
	i32 500358224, ; 53: id/Microsoft.Maui.Controls.resources.dll => 0x1dd2dc50 => 26
	i32 503918385, ; 54: fi/Microsoft.Maui.Controls.resources.dll => 0x1e092f31 => 20
	i32 513247710, ; 55: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 62
	i32 539058512, ; 56: Microsoft.Extensions.Logging => 0x20216150 => 59
	i32 546455878, ; 57: System.Runtime.Serialization.Xml => 0x20924146 => 184
	i32 548916678, ; 58: Microsoft.Bcl.AsyncInterfaces => 0x20b7cdc6 => 51
	i32 592146354, ; 59: pt-BR/Microsoft.Maui.Controls.resources.dll => 0x234b6fb2 => 34
	i32 613668793, ; 60: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 188
	i32 627049542, ; 61: SFSm => 0x25600446 => 126
	i32 627609679, ; 62: Xamarin.AndroidX.CustomView => 0x2568904f => 102
	i32 627931235, ; 63: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 32
	i32 662205335, ; 64: System.Text.Encodings.Web.dll => 0x27787397 => 196
	i32 672442732, ; 65: System.Collections.Concurrent => 0x2814a96c => 130
	i32 683518922, ; 66: System.Net.Security => 0x28bdabca => 163
	i32 688181140, ; 67: ca/Microsoft.Maui.Controls.resources.dll => 0x2904cf94 => 14
	i32 690569205, ; 68: System.Xml.Linq.dll => 0x29293ff5 => 205
	i32 706645707, ; 69: ko/Microsoft.Maui.Controls.resources.dll => 0x2a1e8ecb => 29
	i32 709152836, ; 70: System.Security.Cryptography.Pkcs.dll => 0x2a44d044 => 88
	i32 709557578, ; 71: de/Microsoft.Maui.Controls.resources.dll => 0x2a4afd4a => 17
	i32 722857257, ; 72: System.Runtime.Loader.dll => 0x2b15ed29 => 179
	i32 723796036, ; 73: System.ClientModel.dll => 0x2b244044 => 83
	i32 748832960, ; 74: SQLitePCLRaw.batteries_v2 => 0x2ca248c0 => 79
	i32 759454413, ; 75: System.Net.Requests => 0x2d445acd => 162
	i32 762598435, ; 76: System.IO.Pipes.dll => 0x2d745423 => 154
	i32 775507847, ; 77: System.IO.Compression => 0x2e394f87 => 151
	i32 777317022, ; 78: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 38
	i32 789151979, ; 79: Microsoft.Extensions.Options => 0x2f0980eb => 61
	i32 804715423, ; 80: System.Data.Common => 0x2ff6fb9f => 140
	i32 823281589, ; 81: System.Private.Uri.dll => 0x311247b5 => 171
	i32 830298997, ; 82: System.IO.Compression.Brotli => 0x317d5b75 => 150
	i32 839353180, ; 83: ZXing.Net.MAUI.Controls.dll => 0x3207835c => 125
	i32 865465478, ; 84: zxing.dll => 0x3395f486 => 123
	i32 904024072, ; 85: System.ComponentModel.Primitives.dll => 0x35e25008 => 136
	i32 926902833, ; 86: tr/Microsoft.Maui.Controls.resources.dll => 0x373f6a31 => 41
	i32 928116545, ; 87: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 120
	i32 955402788, ; 88: Newtonsoft.Json => 0x38f24a24 => 77
	i32 967690846, ; 89: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 105
	i32 975236339, ; 90: System.Diagnostics.Tracing => 0x3a20ecf3 => 146
	i32 975874589, ; 91: System.Xml.XDocument => 0x3a2aaa1d => 207
	i32 986514023, ; 92: System.Private.DataContractSerialization.dll => 0x3acd0267 => 170
	i32 992768348, ; 93: System.Collections.dll => 0x3b2c715c => 134
	i32 1012816738, ; 94: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 115
	i32 1019214401, ; 95: System.Drawing => 0x3cbffa41 => 148
	i32 1028951442, ; 96: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 58
	i32 1029334545, ; 97: da/Microsoft.Maui.Controls.resources.dll => 0x3d5a6611 => 16
	i32 1035644815, ; 98: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 91
	i32 1036536393, ; 99: System.Drawing.Primitives.dll => 0x3dc84a49 => 147
	i32 1044663988, ; 100: System.Linq.Expressions.dll => 0x3e444eb4 => 155
	i32 1048439329, ; 101: de/Microsoft.Data.SqlClient.resources.dll => 0x3e7dea21 => 1
	i32 1052210849, ; 102: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 107
	i32 1062017875, ; 103: Microsoft.Identity.Client.Extensions.Msal => 0x3f4d1b53 => 64
	i32 1082857460, ; 104: System.ComponentModel.TypeConverter => 0x408b17f4 => 137
	i32 1084122840, ; 105: Xamarin.Kotlin.StdLib => 0x409e66d8 => 121
	i32 1089913930, ; 106: System.Diagnostics.EventLog.dll => 0x40f6c44a => 85
	i32 1098259244, ; 107: System => 0x41761b2c => 209
	i32 1118262833, ; 108: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 29
	i32 1138436374, ; 109: Microsoft.Data.SqlClient.dll => 0x43db2916 => 52
	i32 1168523401, ; 110: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 35
	i32 1175944061, ; 111: Camera.MAUI => 0x46177b7d => 50
	i32 1178241025, ; 112: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 112
	i32 1203215381, ; 113: pl/Microsoft.Maui.Controls.resources.dll => 0x47b79c15 => 33
	i32 1204575371, ; 114: Microsoft.Extensions.Caching.Memory.dll => 0x47cc5c8b => 54
	i32 1208641965, ; 115: System.Diagnostics.Process => 0x480a69ad => 142
	i32 1234928153, ; 116: nb/Microsoft.Maui.Controls.resources.dll => 0x499b8219 => 31
	i32 1260983243, ; 117: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 15
	i32 1292207520, ; 118: SQLitePCLRaw.core.dll => 0x4d0585a0 => 80
	i32 1293217323, ; 119: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 103
	i32 1309188875, ; 120: System.Private.DataContractSerialization => 0x4e08a30b => 170
	i32 1315359775, ; 121: cs/Microsoft.Data.SqlClient.resources.dll => 0x4e66cc1f => 0
	i32 1324164729, ; 122: System.Linq => 0x4eed2679 => 156
	i32 1335329327, ; 123: System.Runtime.Serialization.Json.dll => 0x4f97822f => 182
	i32 1373134921, ; 124: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 45
	i32 1376866003, ; 125: Xamarin.AndroidX.SavedState => 0x52114ed3 => 115
	i32 1406073936, ; 126: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 99
	i32 1408764838, ; 127: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 181
	i32 1430672901, ; 128: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 13
	i32 1452070440, ; 129: System.Formats.Asn1.dll => 0x568cd628 => 149
	i32 1458022317, ; 130: System.Net.Security.dll => 0x56e7a7ad => 163
	i32 1460893475, ; 131: System.IdentityModel.Tokens.Jwt => 0x57137723 => 86
	i32 1461004990, ; 132: es\Microsoft.Maui.Controls.resources => 0x57152abe => 19
	i32 1461234159, ; 133: System.Collections.Immutable.dll => 0x5718a9ef => 131
	i32 1462112819, ; 134: System.IO.Compression.dll => 0x57261233 => 151
	i32 1469204771, ; 135: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 92
	i32 1470490898, ; 136: Microsoft.Extensions.Primitives => 0x57a5e912 => 62
	i32 1479771757, ; 137: System.Collections.Immutable => 0x5833866d => 131
	i32 1480492111, ; 138: System.IO.Compression.Brotli.dll => 0x583e844f => 150
	i32 1487239319, ; 139: Microsoft.Win32.Primitives => 0x58a57897 => 128
	i32 1493001747, ; 140: hi/Microsoft.Maui.Controls.resources.dll => 0x58fd6613 => 23
	i32 1498168481, ; 141: Microsoft.IdentityModel.JsonWebTokens.dll => 0x594c3ca1 => 66
	i32 1514721132, ; 142: el/Microsoft.Maui.Controls.resources.dll => 0x5a48cf6c => 18
	i32 1536373174, ; 143: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 144
	i32 1543031311, ; 144: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 198
	i32 1551623176, ; 145: sk/Microsoft.Maui.Controls.resources.dll => 0x5c7be408 => 38
	i32 1573704789, ; 146: System.Runtime.Serialization.Json => 0x5dccd455 => 182
	i32 1582305585, ; 147: Azure.Identity => 0x5e501131 => 49
	i32 1596263029, ; 148: zh-Hant\Microsoft.Data.SqlClient.resources => 0x5f250a75 => 12
	i32 1604827217, ; 149: System.Net.WebClient => 0x5fa7b851 => 166
	i32 1622152042, ; 150: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 109
	i32 1624863272, ; 151: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 118
	i32 1628113371, ; 152: Microsoft.IdentityModel.Protocols.OpenIdConnect => 0x610b09db => 69
	i32 1636350590, ; 153: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 101
	i32 1639515021, ; 154: System.Net.Http.dll => 0x61b9038d => 158
	i32 1639986890, ; 155: System.Text.RegularExpressions => 0x61c036ca => 198
	i32 1657153582, ; 156: System.Runtime => 0x62c6282e => 185
	i32 1658251792, ; 157: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 119
	i32 1677501392, ; 158: System.Net.Primitives.dll => 0x63fca3d0 => 161
	i32 1679769178, ; 159: System.Security.Cryptography => 0x641f3e5a => 192
	i32 1711441057, ; 160: SQLitePCLRaw.lib.e_sqlite3.android => 0x660284a1 => 81
	i32 1729485958, ; 161: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 97
	i32 1736233607, ; 162: ro/Microsoft.Maui.Controls.resources.dll => 0x677cd287 => 36
	i32 1743415430, ; 163: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 14
	i32 1744735666, ; 164: System.Transactions.Local.dll => 0x67fe8db2 => 204
	i32 1750313021, ; 165: Microsoft.Win32.Primitives.dll => 0x6853a83d => 128
	i32 1763938596, ; 166: System.Diagnostics.TraceSource.dll => 0x69239124 => 145
	i32 1766324549, ; 167: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 116
	i32 1770582343, ; 168: Microsoft.Extensions.Logging.dll => 0x6988f147 => 59
	i32 1780572499, ; 169: Mono.Android.Runtime.dll => 0x6a216153 => 213
	i32 1782862114, ; 170: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 30
	i32 1788241197, ; 171: Xamarin.AndroidX.Fragment => 0x6a96652d => 104
	i32 1793755602, ; 172: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 22
	i32 1794500907, ; 173: Microsoft.Identity.Client.dll => 0x6af5e92b => 63
	i32 1796167890, ; 174: Microsoft.Bcl.AsyncInterfaces.dll => 0x6b0f58d2 => 51
	i32 1808609942, ; 175: Xamarin.AndroidX.Loader => 0x6bcd3296 => 109
	i32 1813058853, ; 176: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 121
	i32 1813201214, ; 177: Xamarin.Google.Android.Material => 0x6c13413e => 119
	i32 1818569960, ; 178: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 113
	i32 1824175904, ; 179: System.Text.Encoding.Extensions => 0x6cbab720 => 195
	i32 1824722060, ; 180: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 181
	i32 1828688058, ; 181: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 60
	i32 1842015223, ; 182: uk/Microsoft.Maui.Controls.resources.dll => 0x6dcaebf7 => 42
	i32 1853025655, ; 183: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 39
	i32 1858542181, ; 184: System.Linq.Expressions => 0x6ec71a65 => 155
	i32 1870277092, ; 185: System.Reflection.Primitives => 0x6f7a29e4 => 176
	i32 1871986876, ; 186: Microsoft.IdentityModel.Protocols.OpenIdConnect.dll => 0x6f9440bc => 69
	i32 1875935024, ; 187: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 21
	i32 1910275211, ; 188: System.Collections.NonGeneric.dll => 0x71dc7c8b => 132
	i32 1939592360, ; 189: System.Private.Xml.Linq => 0x739bd4a8 => 172
	i32 1968388702, ; 190: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 55
	i32 1986222447, ; 191: Microsoft.IdentityModel.Tokens.dll => 0x7663596f => 70
	i32 2003115576, ; 192: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 18
	i32 2011961780, ; 193: System.Buffers.dll => 0x77ec19b4 => 129
	i32 2019465201, ; 194: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 107
	i32 2025202353, ; 195: ar/Microsoft.Maui.Controls.resources.dll => 0x78b622b1 => 13
	i32 2040764568, ; 196: Microsoft.Identity.Client.Extensions.Msal.dll => 0x79a39898 => 64
	i32 2045470958, ; 197: System.Private.Xml => 0x79eb68ee => 173
	i32 2055257422, ; 198: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 106
	i32 2066184531, ; 199: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 17
	i32 2070888862, ; 200: System.Diagnostics.TraceSource => 0x7b6f419e => 145
	i32 2079903147, ; 201: System.Runtime.dll => 0x7bf8cdab => 185
	i32 2090596640, ; 202: System.Numerics.Vectors => 0x7c9bf920 => 168
	i32 2103459038, ; 203: SQLitePCLRaw.provider.e_sqlite3.dll => 0x7d603cde => 82
	i32 2127167465, ; 204: System.Console => 0x7ec9ffe9 => 139
	i32 2142473426, ; 205: System.Collections.Specialized => 0x7fb38cd2 => 133
	i32 2143790110, ; 206: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 208
	i32 2159891885, ; 207: Microsoft.Maui => 0x80bd55ad => 73
	i32 2169148018, ; 208: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 25
	i32 2181898931, ; 209: Microsoft.Extensions.Options.dll => 0x820d22b3 => 61
	i32 2192057212, ; 210: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 60
	i32 2193016926, ; 211: System.ObjectModel.dll => 0x82b6c85e => 169
	i32 2201107256, ; 212: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 122
	i32 2201231467, ; 213: System.Net.Http => 0x8334206b => 158
	i32 2207618523, ; 214: it\Microsoft.Maui.Controls.resources => 0x839595db => 27
	i32 2228745826, ; 215: pt-BR\Microsoft.Data.SqlClient.resources => 0x84d7f662 => 8
	i32 2253551641, ; 216: Microsoft.IdentityModel.Protocols => 0x86527819 => 68
	i32 2265110946, ; 217: System.Security.AccessControl.dll => 0x8702d9a2 => 186
	i32 2266799131, ; 218: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 56
	i32 2270573516, ; 219: fr/Microsoft.Maui.Controls.resources.dll => 0x875633cc => 21
	i32 2279755925, ; 220: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 114
	i32 2295906218, ; 221: System.Net.Sockets => 0x88d8bfaa => 165
	i32 2303942373, ; 222: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 31
	i32 2305521784, ; 223: System.Private.CoreLib.dll => 0x896b7878 => 211
	i32 2309278602, ; 224: ko\Microsoft.Data.SqlClient.resources => 0x89a4cb8a => 6
	i32 2340441535, ; 225: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 177
	i32 2353062107, ; 226: System.Net.Primitives => 0x8c40e0db => 161
	i32 2368005991, ; 227: System.Xml.ReaderWriter.dll => 0x8d24e767 => 206
	i32 2369706906, ; 228: Microsoft.IdentityModel.Logging => 0x8d3edb9a => 67
	i32 2371007202, ; 229: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 55
	i32 2383496789, ; 230: System.Security.Principal.Windows.dll => 0x8e114655 => 193
	i32 2395872292, ; 231: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 26
	i32 2427813419, ; 232: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 23
	i32 2435356389, ; 233: System.Console.dll => 0x912896e5 => 139
	i32 2458678730, ; 234: System.Net.Sockets.dll => 0x928c75ca => 165
	i32 2465273461, ; 235: SQLitePCLRaw.batteries_v2.dll => 0x92f11675 => 79
	i32 2471841756, ; 236: netstandard.dll => 0x93554fdc => 210
	i32 2475788418, ; 237: Java.Interop.dll => 0x93918882 => 212
	i32 2480646305, ; 238: Microsoft.Maui.Controls => 0x93dba8a1 => 71
	i32 2484371297, ; 239: System.Net.ServicePoint => 0x94147f61 => 164
	i32 2509217888, ; 240: System.Diagnostics.EventLog => 0x958fa060 => 85
	i32 2538310050, ; 241: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 175
	i32 2550873716, ; 242: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 24
	i32 2562349572, ; 243: Microsoft.CSharp => 0x98ba5a04 => 127
	i32 2570120770, ; 244: System.Text.Encodings.Web => 0x9930ee42 => 196
	i32 2585220780, ; 245: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 195
	i32 2589602615, ; 246: System.Threading.ThreadPool => 0x9a5a3337 => 202
	i32 2593496499, ; 247: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 33
	i32 2605712449, ; 248: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 122
	i32 2617129537, ; 249: System.Private.Xml.dll => 0x9bfe3a41 => 173
	i32 2620871830, ; 250: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 101
	i32 2626831493, ; 251: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 28
	i32 2627185994, ; 252: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 144
	i32 2628210652, ; 253: System.Memory.Data => 0x9ca74fdc => 87
	i32 2640290731, ; 254: Microsoft.IdentityModel.Logging.dll => 0x9d5fa3ab => 67
	i32 2640706905, ; 255: Azure.Core => 0x9d65fd59 => 48
	i32 2660759594, ; 256: System.Security.Cryptography.ProtectedData.dll => 0x9e97f82a => 89
	i32 2663698177, ; 257: System.Runtime.Loader => 0x9ec4cf01 => 179
	i32 2664396074, ; 258: System.Xml.XDocument.dll => 0x9ecf752a => 207
	i32 2665622720, ; 259: System.Drawing.Primitives => 0x9ee22cc0 => 147
	i32 2676780864, ; 260: System.Data.Common.dll => 0x9f8c6f40 => 140
	i32 2677098746, ; 261: Azure.Identity.dll => 0x9f9148fa => 49
	i32 2678266992, ; 262: tr\Microsoft.Data.SqlClient.resources => 0x9fa31c70 => 10
	i32 2686887180, ; 263: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 184
	i32 2717744543, ; 264: System.Security.Claims => 0xa1fd7d9f => 187
	i32 2724373263, ; 265: System.Runtime.Numerics.dll => 0xa262a30f => 180
	i32 2732626843, ; 266: Xamarin.AndroidX.Activity => 0xa2e0939b => 90
	i32 2735172069, ; 267: System.Threading.Channels => 0xa30769e5 => 199
	i32 2737747696, ; 268: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 92
	i32 2740051746, ; 269: Microsoft.Identity.Client => 0xa351df22 => 63
	i32 2752995522, ; 270: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 34
	i32 2755098380, ; 271: Microsoft.SqlServer.Server.dll => 0xa437770c => 76
	i32 2758225723, ; 272: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 72
	i32 2764765095, ; 273: Microsoft.Maui.dll => 0xa4caf7a7 => 73
	i32 2765824710, ; 274: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 194
	i32 2778768386, ; 275: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 117
	i32 2785988530, ; 276: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 40
	i32 2801831435, ; 277: Microsoft.Maui.Graphics => 0xa7008e0b => 75
	i32 2804509662, ; 278: es/Microsoft.Data.SqlClient.resources.dll => 0xa7296bde => 2
	i32 2806116107, ; 279: es/Microsoft.Maui.Controls.resources.dll => 0xa741ef0b => 19
	i32 2810250172, ; 280: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 99
	i32 2831556043, ; 281: nl/Microsoft.Maui.Controls.resources.dll => 0xa8c61dcb => 32
	i32 2841937114, ; 282: it/Microsoft.Data.SqlClient.resources.dll => 0xa96484da => 4
	i32 2853208004, ; 283: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 117
	i32 2861189240, ; 284: Microsoft.Maui.Essentials => 0xaa8a4878 => 74
	i32 2867946736, ; 285: System.Security.Cryptography.ProtectedData => 0xaaf164f0 => 89
	i32 2891872470, ; 286: cs\Microsoft.Data.SqlClient.resources => 0xac5e78d6 => 0
	i32 2909740682, ; 287: System.Private.CoreLib => 0xad6f1e8a => 211
	i32 2916838712, ; 288: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 118
	i32 2919462931, ; 289: System.Numerics.Vectors.dll => 0xae037813 => 168
	i32 2937062795, ; 290: SFSm.dll => 0xaf10058b => 126
	i32 2940926066, ; 291: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 143
	i32 2944313911, ; 292: System.Configuration.ConfigurationManager.dll => 0xaf7eaa37 => 84
	i32 2959614098, ; 293: System.ComponentModel.dll => 0xb0682092 => 138
	i32 2965157864, ; 294: Xamarin.AndroidX.Camera.View => 0xb0bcb7e8 => 96
	i32 2968338931, ; 295: System.Security.Principal.Windows => 0xb0ed41f3 => 193
	i32 2972252294, ; 296: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 188
	i32 2978675010, ; 297: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 103
	i32 2991449226, ; 298: Xamarin.AndroidX.Camera.Core => 0xb24de48a => 94
	i32 3000842441, ; 299: Xamarin.AndroidX.Camera.View.dll => 0xb2dd38c9 => 96
	i32 3012788804, ; 300: System.Configuration.ConfigurationManager => 0xb3938244 => 84
	i32 3023511517, ; 301: ru\Microsoft.Data.SqlClient.resources => 0xb4371fdd => 9
	i32 3033605958, ; 302: System.Memory.Data.dll => 0xb4d12746 => 87
	i32 3038032645, ; 303: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 47
	i32 3047751430, ; 304: Xamarin.AndroidX.Camera.Core.dll => 0xb5a8ff06 => 94
	i32 3057625584, ; 305: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 110
	i32 3059408633, ; 306: Mono.Android.Runtime => 0xb65adef9 => 213
	i32 3059793426, ; 307: System.ComponentModel.Primitives => 0xb660be12 => 136
	i32 3069363400, ; 308: Microsoft.Extensions.Caching.Abstractions.dll => 0xb6f2c4c8 => 53
	i32 3077302341, ; 309: hu/Microsoft.Maui.Controls.resources.dll => 0xb76be845 => 25
	i32 3084678329, ; 310: Microsoft.IdentityModel.Tokens => 0xb7dc74b9 => 70
	i32 3090735792, ; 311: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 191
	i32 3099732863, ; 312: System.Security.Claims.dll => 0xb8c22b7f => 187
	i32 3103600923, ; 313: System.Formats.Asn1 => 0xb8fd311b => 149
	i32 3121463068, ; 314: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 152
	i32 3124832203, ; 315: System.Threading.Tasks.Extensions => 0xba4127cb => 200
	i32 3132293585, ; 316: System.Security.AccessControl => 0xbab301d1 => 186
	i32 3147165239, ; 317: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 146
	i32 3158628304, ; 318: zh-Hant/Microsoft.Data.SqlClient.resources.dll => 0xbc44d7d0 => 12
	i32 3159123045, ; 319: System.Reflection.Primitives.dll => 0xbc4c6465 => 176
	i32 3178803400, ; 320: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 111
	i32 3195844289, ; 321: Microsoft.Extensions.Caching.Abstractions => 0xbe7cb6c1 => 53
	i32 3215347189, ; 322: zxing => 0xbfa64df5 => 123
	i32 3220365878, ; 323: System.Threading => 0xbff2e236 => 203
	i32 3258312781, ; 324: Xamarin.AndroidX.CardView => 0xc235e84d => 97
	i32 3265893370, ; 325: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 200
	i32 3268887220, ; 326: fr/Microsoft.Data.SqlClient.resources.dll => 0xc2d742b4 => 3
	i32 3276600297, ; 327: pt-BR/Microsoft.Data.SqlClient.resources.dll => 0xc34cf3e9 => 8
	i32 3280506390, ; 328: System.ComponentModel.Annotations.dll => 0xc3888e16 => 135
	i32 3286373667, ; 329: ZXing.Net.MAUI.dll => 0xc3e21523 => 124
	i32 3286872994, ; 330: SQLite-net.dll => 0xc3e9b3a2 => 78
	i32 3290767353, ; 331: System.Security.Cryptography.Encoding => 0xc4251ff9 => 189
	i32 3305363605, ; 332: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 20
	i32 3312457198, ; 333: Microsoft.IdentityModel.JsonWebTokens => 0xc57015ee => 66
	i32 3316684772, ; 334: System.Net.Requests.dll => 0xc5b097e4 => 162
	i32 3317135071, ; 335: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 102
	i32 3343947874, ; 336: fr\Microsoft.Data.SqlClient.resources => 0xc7509862 => 3
	i32 3346324047, ; 337: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 112
	i32 3357674450, ; 338: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 37
	i32 3358260929, ; 339: System.Text.Json => 0xc82afec1 => 197
	i32 3360279109, ; 340: SQLitePCLRaw.core => 0xc849ca45 => 80
	i32 3362522851, ; 341: Xamarin.AndroidX.Core => 0xc86c06e3 => 100
	i32 3366347497, ; 342: Java.Interop => 0xc8a662e9 => 212
	i32 3374879918, ; 343: Microsoft.IdentityModel.Protocols.dll => 0xc92894ae => 68
	i32 3374999561, ; 344: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 114
	i32 3381016424, ; 345: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 16
	i32 3428513518, ; 346: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 57
	i32 3430777524, ; 347: netstandard => 0xcc7d82b4 => 210
	i32 3463511458, ; 348: hr/Microsoft.Maui.Controls.resources.dll => 0xce70fda2 => 24
	i32 3471940407, ; 349: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 137
	i32 3476120550, ; 350: Mono.Android => 0xcf3163e6 => 214
	i32 3479583265, ; 351: ru/Microsoft.Maui.Controls.resources.dll => 0xcf663a21 => 37
	i32 3484440000, ; 352: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 36
	i32 3485117614, ; 353: System.Text.Json.dll => 0xcfbaacae => 197
	i32 3509114376, ; 354: System.Xml.Linq => 0xd128d608 => 205
	i32 3545306353, ; 355: Microsoft.Data.SqlClient => 0xd35114f1 => 52
	i32 3555084973, ; 356: de\Microsoft.Data.SqlClient.resources => 0xd3e64aad => 1
	i32 3558648585, ; 357: System.ClientModel => 0xd41cab09 => 83
	i32 3561949811, ; 358: Azure.Core.dll => 0xd44f0a73 => 48
	i32 3570554715, ; 359: System.IO.FileSystem.AccessControl => 0xd4d2575b => 152
	i32 3580758918, ; 360: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 44
	i32 3608519521, ; 361: System.Linq.dll => 0xd715a361 => 156
	i32 3624195450, ; 362: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 177
	i32 3641597786, ; 363: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 106
	i32 3643446276, ; 364: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 41
	i32 3643854240, ; 365: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 111
	i32 3657292374, ; 366: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 56
	i32 3660523487, ; 367: System.Net.NetworkInformation => 0xda2f27df => 160
	i32 3672681054, ; 368: Mono.Android.dll => 0xdae8aa5e => 214
	i32 3676461095, ; 369: Xamarin.AndroidX.Camera.Lifecycle => 0xdb225827 => 95
	i32 3682565725, ; 370: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 93
	i32 3697841164, ; 371: zh-Hant/Microsoft.Maui.Controls.resources.dll => 0xdc68940c => 46
	i32 3700591436, ; 372: Microsoft.IdentityModel.Abstractions.dll => 0xdc928b4c => 65
	i32 3724971120, ; 373: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 110
	i32 3732100267, ; 374: System.Net.NameResolution => 0xde7354ab => 159
	i32 3748608112, ; 375: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 141
	i32 3751582913, ; 376: ZXing.Net.MAUI.Controls => 0xdf9c9cc1 => 125
	i32 3754567612, ; 377: SQLitePCLRaw.provider.e_sqlite3 => 0xdfca27bc => 82
	i32 3786282454, ; 378: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 98
	i32 3792276235, ; 379: System.Collections.NonGeneric => 0xe2098b0b => 132
	i32 3802395368, ; 380: System.Collections.Specialized.dll => 0xe2a3f2e8 => 133
	i32 3803019198, ; 381: zh-Hans/Microsoft.Data.SqlClient.resources.dll => 0xe2ad77be => 11
	i32 3807198597, ; 382: System.Security.Cryptography.Pkcs => 0xe2ed3d85 => 88
	i32 3823082795, ; 383: System.Security.Cryptography.dll => 0xe3df9d2b => 192
	i32 3841636137, ; 384: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 58
	i32 3842894692, ; 385: ZXing.Net.MAUI => 0xe50deb64 => 124
	i32 3848348906, ; 386: es\Microsoft.Data.SqlClient.resources => 0xe56124ea => 2
	i32 3849253459, ; 387: System.Runtime.InteropServices.dll => 0xe56ef253 => 178
	i32 3875112723, ; 388: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 189
	i32 3876362041, ; 389: SQLite-net => 0xe70c9739 => 78
	i32 3885497537, ; 390: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 167
	i32 3889960447, ; 391: zh-Hans/Microsoft.Maui.Controls.resources.dll => 0xe7dc15ff => 45
	i32 3896106733, ; 392: System.Collections.Concurrent.dll => 0xe839deed => 130
	i32 3896760992, ; 393: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 100
	i32 3928044579, ; 394: System.Xml.ReaderWriter => 0xea213423 => 206
	i32 3931092270, ; 395: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 113
	i32 3953953790, ; 396: System.Text.Encoding.CodePages => 0xebac8bfe => 194
	i32 3955647286, ; 397: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 91
	i32 3980434154, ; 398: th/Microsoft.Maui.Controls.resources.dll => 0xed409aea => 40
	i32 3987592930, ; 399: he/Microsoft.Maui.Controls.resources.dll => 0xedadd6e2 => 22
	i32 4003436829, ; 400: System.Diagnostics.Process.dll => 0xee9f991d => 142
	i32 4025784931, ; 401: System.Memory => 0xeff49a63 => 157
	i32 4046471985, ; 402: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 72
	i32 4054681211, ; 403: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 174
	i32 4064142224, ; 404: pl/Microsoft.Data.SqlClient.resources.dll => 0xf23de390 => 7
	i32 4068434129, ; 405: System.Private.Xml.Linq.dll => 0xf27f60d1 => 172
	i32 4073602200, ; 406: System.Threading.dll => 0xf2ce3c98 => 203
	i32 4094352644, ; 407: Microsoft.Maui.Essentials.dll => 0xf40add04 => 74
	i32 4099507663, ; 408: System.Drawing.dll => 0xf45985cf => 148
	i32 4100113165, ; 409: System.Private.Uri => 0xf462c30d => 171
	i32 4101842092, ; 410: Microsoft.Extensions.Caching.Memory => 0xf47d24ac => 54
	i32 4102112229, ; 411: pt/Microsoft.Maui.Controls.resources.dll => 0xf48143e5 => 35
	i32 4125707920, ; 412: ms/Microsoft.Maui.Controls.resources.dll => 0xf5e94e90 => 30
	i32 4126470640, ; 413: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 57
	i32 4127667938, ; 414: System.IO.FileSystem.Watcher => 0xf60736e2 => 153
	i32 4142654081, ; 415: Camera.MAUI.dll => 0xf6ebe281 => 50
	i32 4147896353, ; 416: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 174
	i32 4150914736, ; 417: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 42
	i32 4159265925, ; 418: System.Xml.XmlSerializer => 0xf7e95c85 => 208
	i32 4164802419, ; 419: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 153
	i32 4181436372, ; 420: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 183
	i32 4182413190, ; 421: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 108
	i32 4196529839, ; 422: System.Net.WebClient.dll => 0xfa21f6af => 166
	i32 4213026141, ; 423: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 141
	i32 4257443520, ; 424: ko/Microsoft.Data.SqlClient.resources.dll => 0xfdc36ec0 => 6
	i32 4260525087, ; 425: System.Buffers => 0xfdf2741f => 129
	i32 4263231520, ; 426: System.IdentityModel.Tokens.Jwt.dll => 0xfe1bc020 => 86
	i32 4271975918, ; 427: Microsoft.Maui.Controls.dll => 0xfea12dee => 71
	i32 4274976490, ; 428: System.Runtime.Numerics => 0xfecef6ea => 180
	i32 4292120959 ; 429: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 108
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [430 x i32] [
	i32 160, ; 0
	i32 159, ; 1
	i32 120, ; 2
	i32 77, ; 3
	i32 95, ; 4
	i32 201, ; 5
	i32 4, ; 6
	i32 5, ; 7
	i32 143, ; 8
	i32 46, ; 9
	i32 190, ; 10
	i32 75, ; 11
	i32 178, ; 12
	i32 199, ; 13
	i32 5, ; 14
	i32 167, ; 15
	i32 190, ; 16
	i32 135, ; 17
	i32 98, ; 18
	i32 9, ; 19
	i32 116, ; 20
	i32 43, ; 21
	i32 44, ; 22
	i32 138, ; 23
	i32 93, ; 24
	i32 127, ; 25
	i32 175, ; 26
	i32 11, ; 27
	i32 15, ; 28
	i32 43, ; 29
	i32 90, ; 30
	i32 76, ; 31
	i32 28, ; 32
	i32 105, ; 33
	i32 81, ; 34
	i32 27, ; 35
	i32 7, ; 36
	i32 154, ; 37
	i32 204, ; 38
	i32 164, ; 39
	i32 201, ; 40
	i32 157, ; 41
	i32 202, ; 42
	i32 47, ; 43
	i32 10, ; 44
	i32 39, ; 45
	i32 134, ; 46
	i32 104, ; 47
	i32 191, ; 48
	i32 183, ; 49
	i32 209, ; 50
	i32 65, ; 51
	i32 169, ; 52
	i32 26, ; 53
	i32 20, ; 54
	i32 62, ; 55
	i32 59, ; 56
	i32 184, ; 57
	i32 51, ; 58
	i32 34, ; 59
	i32 188, ; 60
	i32 126, ; 61
	i32 102, ; 62
	i32 32, ; 63
	i32 196, ; 64
	i32 130, ; 65
	i32 163, ; 66
	i32 14, ; 67
	i32 205, ; 68
	i32 29, ; 69
	i32 88, ; 70
	i32 17, ; 71
	i32 179, ; 72
	i32 83, ; 73
	i32 79, ; 74
	i32 162, ; 75
	i32 154, ; 76
	i32 151, ; 77
	i32 38, ; 78
	i32 61, ; 79
	i32 140, ; 80
	i32 171, ; 81
	i32 150, ; 82
	i32 125, ; 83
	i32 123, ; 84
	i32 136, ; 85
	i32 41, ; 86
	i32 120, ; 87
	i32 77, ; 88
	i32 105, ; 89
	i32 146, ; 90
	i32 207, ; 91
	i32 170, ; 92
	i32 134, ; 93
	i32 115, ; 94
	i32 148, ; 95
	i32 58, ; 96
	i32 16, ; 97
	i32 91, ; 98
	i32 147, ; 99
	i32 155, ; 100
	i32 1, ; 101
	i32 107, ; 102
	i32 64, ; 103
	i32 137, ; 104
	i32 121, ; 105
	i32 85, ; 106
	i32 209, ; 107
	i32 29, ; 108
	i32 52, ; 109
	i32 35, ; 110
	i32 50, ; 111
	i32 112, ; 112
	i32 33, ; 113
	i32 54, ; 114
	i32 142, ; 115
	i32 31, ; 116
	i32 15, ; 117
	i32 80, ; 118
	i32 103, ; 119
	i32 170, ; 120
	i32 0, ; 121
	i32 156, ; 122
	i32 182, ; 123
	i32 45, ; 124
	i32 115, ; 125
	i32 99, ; 126
	i32 181, ; 127
	i32 13, ; 128
	i32 149, ; 129
	i32 163, ; 130
	i32 86, ; 131
	i32 19, ; 132
	i32 131, ; 133
	i32 151, ; 134
	i32 92, ; 135
	i32 62, ; 136
	i32 131, ; 137
	i32 150, ; 138
	i32 128, ; 139
	i32 23, ; 140
	i32 66, ; 141
	i32 18, ; 142
	i32 144, ; 143
	i32 198, ; 144
	i32 38, ; 145
	i32 182, ; 146
	i32 49, ; 147
	i32 12, ; 148
	i32 166, ; 149
	i32 109, ; 150
	i32 118, ; 151
	i32 69, ; 152
	i32 101, ; 153
	i32 158, ; 154
	i32 198, ; 155
	i32 185, ; 156
	i32 119, ; 157
	i32 161, ; 158
	i32 192, ; 159
	i32 81, ; 160
	i32 97, ; 161
	i32 36, ; 162
	i32 14, ; 163
	i32 204, ; 164
	i32 128, ; 165
	i32 145, ; 166
	i32 116, ; 167
	i32 59, ; 168
	i32 213, ; 169
	i32 30, ; 170
	i32 104, ; 171
	i32 22, ; 172
	i32 63, ; 173
	i32 51, ; 174
	i32 109, ; 175
	i32 121, ; 176
	i32 119, ; 177
	i32 113, ; 178
	i32 195, ; 179
	i32 181, ; 180
	i32 60, ; 181
	i32 42, ; 182
	i32 39, ; 183
	i32 155, ; 184
	i32 176, ; 185
	i32 69, ; 186
	i32 21, ; 187
	i32 132, ; 188
	i32 172, ; 189
	i32 55, ; 190
	i32 70, ; 191
	i32 18, ; 192
	i32 129, ; 193
	i32 107, ; 194
	i32 13, ; 195
	i32 64, ; 196
	i32 173, ; 197
	i32 106, ; 198
	i32 17, ; 199
	i32 145, ; 200
	i32 185, ; 201
	i32 168, ; 202
	i32 82, ; 203
	i32 139, ; 204
	i32 133, ; 205
	i32 208, ; 206
	i32 73, ; 207
	i32 25, ; 208
	i32 61, ; 209
	i32 60, ; 210
	i32 169, ; 211
	i32 122, ; 212
	i32 158, ; 213
	i32 27, ; 214
	i32 8, ; 215
	i32 68, ; 216
	i32 186, ; 217
	i32 56, ; 218
	i32 21, ; 219
	i32 114, ; 220
	i32 165, ; 221
	i32 31, ; 222
	i32 211, ; 223
	i32 6, ; 224
	i32 177, ; 225
	i32 161, ; 226
	i32 206, ; 227
	i32 67, ; 228
	i32 55, ; 229
	i32 193, ; 230
	i32 26, ; 231
	i32 23, ; 232
	i32 139, ; 233
	i32 165, ; 234
	i32 79, ; 235
	i32 210, ; 236
	i32 212, ; 237
	i32 71, ; 238
	i32 164, ; 239
	i32 85, ; 240
	i32 175, ; 241
	i32 24, ; 242
	i32 127, ; 243
	i32 196, ; 244
	i32 195, ; 245
	i32 202, ; 246
	i32 33, ; 247
	i32 122, ; 248
	i32 173, ; 249
	i32 101, ; 250
	i32 28, ; 251
	i32 144, ; 252
	i32 87, ; 253
	i32 67, ; 254
	i32 48, ; 255
	i32 89, ; 256
	i32 179, ; 257
	i32 207, ; 258
	i32 147, ; 259
	i32 140, ; 260
	i32 49, ; 261
	i32 10, ; 262
	i32 184, ; 263
	i32 187, ; 264
	i32 180, ; 265
	i32 90, ; 266
	i32 199, ; 267
	i32 92, ; 268
	i32 63, ; 269
	i32 34, ; 270
	i32 76, ; 271
	i32 72, ; 272
	i32 73, ; 273
	i32 194, ; 274
	i32 117, ; 275
	i32 40, ; 276
	i32 75, ; 277
	i32 2, ; 278
	i32 19, ; 279
	i32 99, ; 280
	i32 32, ; 281
	i32 4, ; 282
	i32 117, ; 283
	i32 74, ; 284
	i32 89, ; 285
	i32 0, ; 286
	i32 211, ; 287
	i32 118, ; 288
	i32 168, ; 289
	i32 126, ; 290
	i32 143, ; 291
	i32 84, ; 292
	i32 138, ; 293
	i32 96, ; 294
	i32 193, ; 295
	i32 188, ; 296
	i32 103, ; 297
	i32 94, ; 298
	i32 96, ; 299
	i32 84, ; 300
	i32 9, ; 301
	i32 87, ; 302
	i32 47, ; 303
	i32 94, ; 304
	i32 110, ; 305
	i32 213, ; 306
	i32 136, ; 307
	i32 53, ; 308
	i32 25, ; 309
	i32 70, ; 310
	i32 191, ; 311
	i32 187, ; 312
	i32 149, ; 313
	i32 152, ; 314
	i32 200, ; 315
	i32 186, ; 316
	i32 146, ; 317
	i32 12, ; 318
	i32 176, ; 319
	i32 111, ; 320
	i32 53, ; 321
	i32 123, ; 322
	i32 203, ; 323
	i32 97, ; 324
	i32 200, ; 325
	i32 3, ; 326
	i32 8, ; 327
	i32 135, ; 328
	i32 124, ; 329
	i32 78, ; 330
	i32 189, ; 331
	i32 20, ; 332
	i32 66, ; 333
	i32 162, ; 334
	i32 102, ; 335
	i32 3, ; 336
	i32 112, ; 337
	i32 37, ; 338
	i32 197, ; 339
	i32 80, ; 340
	i32 100, ; 341
	i32 212, ; 342
	i32 68, ; 343
	i32 114, ; 344
	i32 16, ; 345
	i32 57, ; 346
	i32 210, ; 347
	i32 24, ; 348
	i32 137, ; 349
	i32 214, ; 350
	i32 37, ; 351
	i32 36, ; 352
	i32 197, ; 353
	i32 205, ; 354
	i32 52, ; 355
	i32 1, ; 356
	i32 83, ; 357
	i32 48, ; 358
	i32 152, ; 359
	i32 44, ; 360
	i32 156, ; 361
	i32 177, ; 362
	i32 106, ; 363
	i32 41, ; 364
	i32 111, ; 365
	i32 56, ; 366
	i32 160, ; 367
	i32 214, ; 368
	i32 95, ; 369
	i32 93, ; 370
	i32 46, ; 371
	i32 65, ; 372
	i32 110, ; 373
	i32 159, ; 374
	i32 141, ; 375
	i32 125, ; 376
	i32 82, ; 377
	i32 98, ; 378
	i32 132, ; 379
	i32 133, ; 380
	i32 11, ; 381
	i32 88, ; 382
	i32 192, ; 383
	i32 58, ; 384
	i32 124, ; 385
	i32 2, ; 386
	i32 178, ; 387
	i32 189, ; 388
	i32 78, ; 389
	i32 167, ; 390
	i32 45, ; 391
	i32 130, ; 392
	i32 100, ; 393
	i32 206, ; 394
	i32 113, ; 395
	i32 194, ; 396
	i32 91, ; 397
	i32 40, ; 398
	i32 22, ; 399
	i32 142, ; 400
	i32 157, ; 401
	i32 72, ; 402
	i32 174, ; 403
	i32 7, ; 404
	i32 172, ; 405
	i32 203, ; 406
	i32 74, ; 407
	i32 148, ; 408
	i32 171, ; 409
	i32 54, ; 410
	i32 35, ; 411
	i32 30, ; 412
	i32 57, ; 413
	i32 153, ; 414
	i32 50, ; 415
	i32 174, ; 416
	i32 42, ; 417
	i32 208, ; 418
	i32 153, ; 419
	i32 183, ; 420
	i32 108, ; 421
	i32 166, ; 422
	i32 141, ; 423
	i32 6, ; 424
	i32 129, ; 425
	i32 86, ; 426
	i32 71, ; 427
	i32 180, ; 428
	i32 108 ; 429
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+armv7-a,+d32,+dsp,+fp64,+neon,+vfp2,+vfp2sp,+vfp3,+vfp3d16,+vfp3d16sp,+vfp3sp,-aes,-fp-armv8,-fp-armv8d16,-fp-armv8d16sp,-fp-armv8sp,-fp16,-fp16fml,-fullfp16,-sha2,-thumb-mode,-vfp4,-vfp4d16,-vfp4d16sp,-vfp4sp" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.2xx @ 96b6bb65e8736e45180905177aa343f0e1854ea3"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"min_enum_size", i32 4}
