namespace NippoControlSystem.UI.Controls
{
    public class ClickAbleButton : Button
    {
        //Clickイベントを発生させる
        public void DoClick()
        {
            this.OnClick(new EventArgs());
        }
    }
}
