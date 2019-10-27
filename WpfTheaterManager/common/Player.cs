using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTheaterManager.data;
using System.IO;

namespace WpfTheaterManager.common
{
    public class Player
    {
        public List<PlayerInfo> listPlayer;

        public Player()
        {
            listPlayer = new List<PlayerInfo>();

            listPlayer.Add(new PlayerInfo("WMP", "wmplayer.exe"));
            listPlayer.Add(new PlayerInfo("GOM", "GOM.exe"));
        }
        public List<PlayerInfo> GetPlayers()
        {
            return listPlayer;
        }
        public static void Execute(Akb48Data myData, string myPlayerName)
        {
            string executePathname = "";

            List<string> listPath = new List<string>{ "D:\\DATA\\Downloads\\AKB48", "F:\\AKB48公演"};

            foreach(string path in listPath)
            {
                executePathname = Path.Combine(path, myData.Filename);

                if (File.Exists(executePathname))
                    break;
            }

            if (executePathname.Length <= 0)
                return;

            Process.Start(@executePathname);

            /*
            // 複数ファイルのためPlayerに対応したリストを作成する
            if (myMovieContents.ExistMovie != null && myMovieContents.ExistMovie.Length > 1)
            {
                string[] arrTargetExt = null;
                arrTargetExt = new string[1];
                arrTargetExt[0] = myMovieContents.Name + "*" + myMovieContents.Extension;

                // プレイリストは一時ディレクトリに書き込むのでパスを取得
                //string tempPath = Path.GetTempPath();

                if (myPlayerName.Equals("GOM"))
                    executePathname = PlayList.MakeAsxFile(myMovieContents.Label, arrTargetExt, Path.GetTempPath(), myMovieContents.Name);
                else if (myPlayerName.Equals("WMP"))
                    executePathname = PlayList.MakeWplFile(myMovieContents.Label, arrTargetExt, Path.GetTempPath(), myMovieContents.Name);
            }
            else if (myMovieContents.ExistMovie != null && myMovieContents.ExistMovie.Length == 1)
            {
                executePathname = myMovieContents.ExistMovie[0];
            }
            else
            {
                if (String.IsNullOrEmpty(myMovieContents.ExistList))
                    return;

                SiteDetail ColViewSiteDetail = new SiteDetail(myMovieContents.Path);

                if (!String.IsNullOrEmpty(myMovieContents.ExistList))
                {
                    List<FileInfo> files = new List<FileInfo>();
                    StreamReader sreader = new StreamReader(myMovieContents.ExistList);
                    string line = sreader.ReadLine();
                    while (line != null)
                    {
                        string movieFilename = Path.Combine(myMovieContents.Path, line);
                        FileInfo fileinfo = new FileInfo(movieFilename);
                        if (fileinfo.Exists)
                            files.Add(fileinfo);

                        line = sreader.ReadLine();
                    }
                    if (myPlayerName.Equals("GOM"))
                        executePathname = PlayList.MakeAsxFile(myMovieContents.Label, files, Path.GetTempPath(), myMovieContents.Name);
                    else if (myPlayerName.Equals("WMP"))
                        executePathname = PlayList.MakeWplFile(myMovieContents.Label, files, Path.GetTempPath(), myMovieContents.Name);
                    Process.Start(@executePathname);
                    return;
                }
                else if (ColViewSiteDetail.ListCount >= 1)
                {
                    List<common.FileContents> list = ColViewSiteDetail.GetList();

                    // Playerリストが存在する場合はPlayerの選択を無視して再生実行
                    if (list.Count >= 1)
                    {
                        executePathname = list[0].FileInfo.FullName;
                        Process.Start(@executePathname);
                        return;
                    }
                }
            }

            var targets = from player in listPlayer
                          where player.Name.ToUpper() == myPlayerName.ToUpper()
                          select player;

            foreach (PlayerInfo info in targets)
            {
                // 起動するファイル名の前後を""でくくる  例) test.mp4 --> "test.mp4"
                Process.Start(info.ExecuteName, "\"" + @executePathname + "\"");
                break;
            }
             */
        }
    }

    public class PlayerInfo
    {
        public PlayerInfo(string myName, string myExecuteName)
        {
            Name = myName;
            ExecuteName = myExecuteName;
        }
        public string Name;
        public string ExecuteName;
    }
}
