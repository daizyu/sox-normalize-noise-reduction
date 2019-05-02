using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Globalization;

namespace ConsoleApp1
{
    class Program
    {
        public List<string> _list = new List<string>();

        static void make_noise_profile(string sox_path,string noise_wave_path, string noise_profile_path)
        {
            bool needMakeProfile = false;

            if (!File.Exists(noise_profile_path))
            {
                needMakeProfile = true;
            }
            else if(System.IO.File.GetLastWriteTime(noise_wave_path)> System.IO.File.GetLastWriteTime(noise_profile_path))
            {
                needMakeProfile = true;
            }
            if (needMakeProfile) { 
                var app = new ProcessStartInfo();
                app.FileName = sox_path;
                app.FileName = sox_path;
                app.Arguments = "\"{WAVE_PATH}\" -n noiseprof \"{PROFILE_PATH}\"";
                app.Arguments = app.Arguments.Replace("{WAVE_PATH}", noise_wave_path);
                app.Arguments = app.Arguments.Replace("{PROFILE_PATH}", noise_profile_path);
                app.UseShellExecute = true;
                var p = Process.Start(app);
                p.WaitForExit();
            }
        }

        static string noise_reduction(string sox_path, string wave_path, string noise_profile_path)
        {
            // sox infile.flac outfile.flac noisered noise_prof 0.4
            string tempFileOrg = Path.GetTempFileName();
            string tempFile = tempFileOrg + ".wav";
            var app = new ProcessStartInfo();
            app.FileName = sox_path;
            app.Arguments = "\"{WAVE_PATH}\" \"{TEMP_PATH}\" noisered {PROFILE_PATH} 0.3";
            app.Arguments = app.Arguments.Replace("{WAVE_PATH}", wave_path);
            app.Arguments = app.Arguments.Replace("{TEMP_PATH}", tempFile);
            app.Arguments = app.Arguments.Replace("{PROFILE_PATH}", noise_profile_path);
            app.UseShellExecute = true;
            var p = Process.Start(app);
            p.WaitForExit();

            File.Delete(tempFileOrg);

            return tempFile;
        }

        static string normalize(string sox_path, string wave_path)
        {
            // sox in.wav out.wav gain -n
            string tempFileOrg = Path.GetTempFileName();
            string tempFile = tempFileOrg + ".wav";

            var app = new ProcessStartInfo();
            app.FileName = sox_path;
            app.Arguments = "\"{WAVE_PATH}\" \"{TEMP_PATH}\" gain -n";
            app.Arguments = app.Arguments.Replace("{WAVE_PATH}", wave_path);
            app.Arguments = app.Arguments.Replace("{TEMP_PATH}", tempFile);
            app.UseShellExecute = true;
            var p = Process.Start(app);
            p.WaitForExit();

            File.Delete(tempFileOrg);

            return tempFile;
        }

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Write("calibration filename\n");
                return;
            }
            string base_dir = System.AppDomain.CurrentDomain.BaseDirectory;
            string sox_path = Path.Combine(base_dir, "sox.exe");
            string wave_file = args[0];

            string temp_path = normalize(sox_path, wave_file);

               
            for (int idx = 1; idx < 10; idx++) {
                string noise_wave_path = Path.Combine(base_dir, "noise" + idx + ".wav");
                string noise_profile_path = Path.Combine(base_dir, "noise" + idx + ".profile");
                if (File.Exists(noise_wave_path))
                {
                    make_noise_profile(sox_path, noise_wave_path, noise_profile_path);
                   
                    string new_temp_path = noise_reduction(sox_path, temp_path, noise_profile_path);
                    File.Delete(temp_path);
                    temp_path = new_temp_path;
                }
            }
            
            string backup_file = Path.Combine(
                Path.GetDirectoryName(wave_file),
                Path.GetFileNameWithoutExtension(wave_file) + DateTime.Now.ToString(" - yyyyMMdd-HHmm.fff") + ".wav");
            File.Copy(wave_file, backup_file,true);
            File.Copy(temp_path, wave_file,true);
            File.Delete(temp_path);
        }
    }
}
