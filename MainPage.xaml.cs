using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RuCoder.Resources;

namespace RuCoder
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void but_from_Click(object sender, EventArgs e)
        {
            text_in.Text = Clipboard.GetText();
        }

        private void but_to_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(text_out.Text);
        }

        private void but_convert_Click(object sender, RoutedEventArgs e)
        {
            but_convert.Focus();
            bool word = false;
            String text = "";
            text_out.Text = "";
            for (int i = 0; i < text_in.Text.Length; i++)
            {
                char c = text_in.Text[i];

                if ((c > 'A' && c < 'Z') || (c > 'a' && c < 'z') || (c > 'А' && c < 'Я') || (c > 'а' && c < 'я'))
                {
                    word = true;
                    text += c;
                }
                else
                {
                    if (word)
                    {
                        word = false;
                        text_out.Text += Coder(text);
                        text = "";
                    }
                    text_out.Text += c;
                }
            }

            if (word)
                text_out.Text += Coder(text);
        }

        String Coder(String in_text)
        {
            if (in_text.Length < 4) return in_text;

            int trim = 1;
            if (in_text.Length > 8)
                trim = 2;

            char[] tmp = in_text.ToCharArray(trim, in_text.Length - trim - 1);
            String start = in_text.Substring(0, trim);
            String end = in_text.Substring(in_text.Length - trim, trim);
            String out_text;
            int cnt = 0;

            do
            {
                Random random = new Random(DateTime.Now.Millisecond);
                tmp = tmp.OrderBy(x => random.Next()).ToArray<char>();
                out_text = start + (new String(tmp)) + end;
                cnt++;
            } while (in_text.Equals(out_text) && cnt < 3);

            return out_text;
        }

        private void but_coder_Click(object sender, EventArgs e)
        {
            but_convert_Click(sender, null);
        }
    }
}