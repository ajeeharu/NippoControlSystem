using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cyc.Forms
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
