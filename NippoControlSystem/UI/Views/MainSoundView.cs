using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.ComponentModel;

#pragma warning disable
#nullable disable // C# 8.0以降のNull許容警告も消す場合

namespace NippoControlSystem.UI.Views
{
    public partial class MainView : Form
    {
        //　------　MVVMパターン用にリファクタリングしたコード　------

        //　------　MVVM化のためにリファクタリングする前のコード　------

        private System.Media.SoundPlayer player = null;

        //OK.WAVファイルを再生する
        private void PlaySoundOk()
        {
            if (Default.PlaySoundEnable[0] == 'Y')
            {
                PlaySound(Default.ApplicationFloder + Default.okWavlPath);
            }
        }

        //NG.WAVファイルを再生する
        private void PlaySoundNg()
        {
            if (Default.PlaySoundEnable[0] == 'Y')
            {
                PlaySound(Default.ApplicationFloder + Default.ngWavlPath);
            }
        }

        //pause.WAVファイルを再生する 20180907
        private void PlaySoundPause(string WavFileName)
        {
            if (Default.PlaySoundEnable[0] == 'Y')
            {

                string wavPath = Default.ApplicationFloder + Default.SettingsHolder + WavFileName;
                if (WavFileName != null && System.IO.File.Exists(wavPath))
                {
                    PlaySound(wavPath);
                }
                else
                {
                    PlaySound(Default.ApplicationFloder + Default.pauseWavPath);
                }
            }
        }

        //pause.WAVファイルを再生する
        private void PlaySoundPause()
        {
            if (Default.PlaySoundEnable[0] == 'Y')
            {
                PlaySound(Default.ApplicationFloder + Default.pauseWavPath);
            }
        }

        //WAVEファイルを再生する
        private void PlaySound(string waveFile)
        {
            //再生されているときは止める
            if (player != null)
                StopSound();

            //読み込む
            player = new System.Media.SoundPlayer(waveFile);
            //非同期再生する
            player.Play();

            //次のようにすると、ループ再生される
            //player.PlayLooping();

            //次のようにすると、最後まで再生し終えるまで待機する
            //player.PlaySync();
        }

        //再生されている音を止める
        private void StopSound()
        {
            if (player != null)
            {
                player.Stop();
                player.Dispose();
                player = null;
            }
        }
    }
}
