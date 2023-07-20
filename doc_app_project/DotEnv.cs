using Microsoft.Extensions.FileProviders;
using Microsoft.VisualBasic;

namespace doc_app_project
{
    public static class DotEnv
    {
        public static string static_file_pointer = "assets";
        public static string static_image_pointer = "image";
        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(
                    '=',
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                    continue;

                Console.WriteLine(parts[0]+ "  " + parts[1] );
                EnvData.data[parts[0]] = parts[1];
                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }

        public static void setupStaticFile(IApplicationBuilder app,string html_path,string staic_path)
        {
            string[] dirList = Directory.GetDirectories(Path.Combine(html_path, static_file_pointer));
            string[] fileList = Directory.GetFiles(Path.Combine(html_path, static_file_pointer));

            Console.WriteLine("setupStaticFile:start");
            foreach (string fileName in fileList)
            {
                Console.WriteLine(fileName);
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(fileName),
                    RequestPath = "/"+static_file_pointer+"/"+Path.GetFileName(fileName)
                });
            }

            foreach (string dirName in dirList)
            {
                string dirSortName = Path.GetFileName(dirName);
                Console.WriteLine(dirSortName+"  "+static_image_pointer);
                if (dirSortName != static_image_pointer)
                {
                    app.UseStaticFiles(new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(dirName),
                        RequestPath = "/" + static_file_pointer + "/" + dirSortName
                    });
                }
                else
                {
                    string reqName = "/" + static_file_pointer + "/" + dirSortName;
                    Console.WriteLine("binding "+reqName+ "  " + staic_path);
                    app.UseStaticFiles(new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(staic_path),
                        RequestPath = reqName
                    });
                    //setUpStaticImagePath(app, Path.Combine(html_path, static_file_pointer), staic_path);
                }
            }
            Console.WriteLine("setupStaticFile:end");
        }

        public static void setUpStaticImagePath(IApplicationBuilder app, string current_path, string staic_path)
        {
            Console.WriteLine("setUpStaticImagePath:start");

            string[] cdirList = Directory.GetDirectories(Path.Combine(current_path, static_image_pointer));
            string[] cfileList = Directory.GetFiles(Path.Combine(current_path, static_image_pointer));

            string[] sdirList = Directory.GetDirectories(Path.Combine(staic_path));
            string[] sfileList = Directory.GetFiles(Path.Combine(staic_path));
              
          Dictionary<string,Boolean> fileMap= new Dictionary<string,Boolean>(); 
            foreach(string dirName in sdirList)
            {
                fileMap[Path.GetDirectoryName(dirName)] = true;
            }
            foreach(string fileName in sfileList)
            {
                fileMap[Path.GetFileName(fileName)] = true;
            }

            foreach(string dirName in cdirList)
            {
                string dirShorName = Path.GetFileName(dirName);
                string phy_path= Path.Combine(current_path,static_image_pointer, dirName);
                string reqName = "/" + static_file_pointer + "/" + static_image_pointer + "/" + dirShorName;
                if (fileMap.ContainsKey(dirShorName)) {
                    phy_path = Path.Combine(staic_path,dirName);
                }
                Console.WriteLine(reqName+" "+phy_path);
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(phy_path),
                    RequestPath = reqName
                });
            }

            foreach (string dirName in cfileList)
            {
                string dirShorName = Path.GetFileName(dirName);
                string phy_path = Path.Combine(current_path, static_image_pointer, dirName);
                string reqName = "/" + static_file_pointer + "/" + static_image_pointer + "/" + dirShorName;
                if (fileMap.ContainsKey(dirShorName))
                {
                    phy_path = Path.Combine(staic_path, dirName);
                }
                Console.WriteLine(reqName + " " + phy_path);

                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(phy_path),
                    RequestPath = reqName
                });
            }

            Console.WriteLine("setUpStaticImagePath:end");

        }

    }
}
