using Android.App;
using Android.Content.Res;
using Android.Runtime;

namespace SFSm
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
            //Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            //{
            //    if (view is Entry)
            //    {
            //        //transparent line
            //        handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);

            //        //disable show keyboard when focus on entry
            //        handler.PlatformView.ShowSoftInputOnFocus = false;
            //    }
            //});
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
