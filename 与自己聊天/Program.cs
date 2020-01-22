using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;


namespace 与自己聊天
{
    class Program
    {
        static int times = 0;
        static DirectoryInfo dirinfo = new DirectoryInfo(".\\diary");
        static FileSystemInfo[] fsinfos = dirinfo.GetFileSystemInfos();
        class fun
        {
            static public void print(string str)
            {
                char[] words = str.ToCharArray();
                Console.Write(">");
                for (int i = 0; i < words.Length; i++)
                {

                    Console.Write(words[i]);
                    if (words[i] == '，' || words[i] =='.')
                        Thread.Sleep(275);
                    else if(words[i] == '。' || words[i] =='？' || words[i] =='！')
                    {
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Thread.Sleep(60);
                    }
                }
                Console.WriteLine();
            }
        }
        static void ShowTimes()
        {
            Console.WriteLine();
            if (times == 0)
            {
                fun.print("嗨，初次见面。");
                fun.print("这是一个，目的为“记录日记”的程序，现在您打开它了。");
            }
            else if (times <= 5)
            {
                fun.print("这是我们的第" + (times+1) + "次见面。我记着呢。");
                fun.print("现在也许您还不太习惯，不过会有习惯的那一天的。");
                Thread.Sleep(860);
                fun.print(";)");
                Thread.Sleep(2000);
            }
            else if (times <= 10)
            {
                fun.print("这是第" + times + "次啦。");
                fun.print("看来有很多话想要说呢。");
                fun.print("那么现在...");
            }
        }
        static void WriteDiary()
        {
            string[] cache = new string[1000];
            int j = -1;
            do
            {
                j++;
                cache[j] = Console.ReadLine();
            } while (cache[j] != "/end");

            DateTime dt = DateTime.Now;
            string datime = dt.ToString("yyyy-MM-dd");

            FileStream fs = new FileStream(".\\diary\\" + datime + ".txt", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.WriteLine(dt.ToString("hh:mm:ss"));
            for (int i = 0; cache[i] != "/end"; i++)
            {
                sw.WriteLine(cache[i]);
            }
            sw.WriteLine("\n");
            sw.Close();
            fs.Close();
        }
        static void ReadDiary(string s)
        {
            FileStream fs = new FileStream(".\\diary\\" + s, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                fun.print(line);
            }
            sr.Close();
            fs.Close();
        }
        static void ShowList()
        {
            for (int i = 0; i < fsinfos.Length; i++)
            {
                Console.WriteLine(i+1 +"\t" +fsinfos[i].Name);
            }
        }

        static void Main(string[] args)
        {
            //设定日志文件格式：程序打开次数 | 日记数量 | 打开时间
            string[] lines = File.ReadAllLines("record.txt");
            times = lines.Length-2;
            FileStream fs = new FileStream("record.txt", FileMode.Append);
            StreamWriter sr = new StreamWriter(fs, Encoding.Default);
            sr.WriteLine(DateTime.Now.ToString());
            sr.Close();
            fs.Close();

            Console.WriteLine();
            fun.print("系统载入中...");
            ShowTimes();
            fun.print("是有什么要倾诉的话语吗？");
            Console.WriteLine(">>>1.想记日记。\n>>>2.想看日记。\n>>>3.随便打开一篇日记吧。\n");
            char choice = Console.ReadLine()[0];
            int errtime = 0;
            do
            {
                switch (choice)
                {
                    case '1':
                        //StreamWriter sw = new StreamWriter("record.txt");
                        fun.print("那么，写下你想倾诉的话语吧。换行输入/end代表你写完了。");
                        WriteDiary();
                        Console.Clear();
                        fun.print("写好了吗？接下来做什么？");
                        Console.WriteLine(">>>0.退出\n>>>1.想记日记。\n>>>2.想看日记。\n>>>3.随便打开一篇日记吧。\n");
                        break;
                    case '2':
                        Console.Clear();
                        Console.WriteLine("选择序号查看相应的日记\n=========================");
                        ShowList();
                        int namenum = int.Parse(Console.ReadLine());
                        ReadDiary(fsinfos[namenum-1].Name);
                        fun.print("以上是日记的全部了。接下来做什么？\n");
                        Console.WriteLine(">>>0.退出\n>>>1.想记日记。\n>>>2.想看日记。\n>>>3.随便打开一篇日记吧。\n");
                        break;
                    case '3':
                        Console.Clear();
                        Random random = new Random();
                        int r = random.Next(fsinfos.Length - 1);
                        fun.print(fsinfos[r].Name);
                        ReadDiary(fsinfos[r].Name);
                        fun.print("以上是日记的全部了。接下来做什么？\n");
                        Console.WriteLine(">>>0.退出\n>>>1.想记日记。\n>>>2.想看日记。\n>>>3.随便打开一篇日记吧。\n");
                        break;
                    default:
                        errtime++;
                        fun.print("不对哦，请选一个可选的选项。");
                        Console.WriteLine(">>>0.退出\n>>>1.想记日记。\n>>>2.想看日记。\n>>>3.随便打开一篇日记吧。\n");
                        break;
                }
            } while((choice = Console.ReadLine()[0]) != '0');
            
            
            
            //F.Close();
        }
    }
}
