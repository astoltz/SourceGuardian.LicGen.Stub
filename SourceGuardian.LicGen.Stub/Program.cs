using System.Text;

namespace SourceGuardian.LicGen.Stub;

class Program
{
    private static readonly HashSet<string> ValueOptions = new(StringComparer.OrdinalIgnoreCase)
    {
        "--expire", 
        "--domain", 
        "--ip", 
        "--mac", 
        "--machine-id", 
        "--projid", 
        "--projkey", 
        "--const", 
        "--time-server", 
        "--text",
        "--docker-socket", 
        "--license-file", 
        "--license-username", 
        "--license-password"
    };

    static int Main(string[] args)
    {
        // Check for help or version flags immediately
        if (args.Length == 0 || args.Contains("-h") || args.Contains("--help"))
        {
            PrintHelp();
            return 0;
        }

        if (args.Contains("-v"))
        {
            Console.WriteLine("SourceGuardian PRO 17.0.0 Script License Generator (Dec 11 2025 18:09:31)");
            return 0;
        }

        // Mimic standard output header
        Console.WriteLine("SourceGuardian PRO 17.0.0 Script License Generator (Dec 11 2025 18:09:31)");
        Console.WriteLine("MOCK VERSION - NOT REAL SOURCEGUARDIAN");
        Console.WriteLine();

        string? outputFile = null;
        var parameters = new Dictionary<string, List<string>>();
        
        for (int i = 0; i < args.Length; i++)
        {
            string arg = args[i];
            if (arg.StartsWith("-"))
            {
                string key = arg;
                string value = "true"; // Default for boolean flags

                if (ValueOptions.Contains(key))
                {
                    // We consume the next argument as value
                    if (i + 1 < args.Length)
                    {
                        value = args[i + 1];
                        i++;
                    }
                    else
                    {
                        Console.Error.WriteLine($"Error: Option {key} requires a value.");
                        return 1;
                    }
                }
                
                if (!parameters.ContainsKey(key))
                {
                    parameters[key] = new List<string>();
                }
                parameters[key].Add(value);
            }
            else
            {
                // Assume positional argument is the output file
                outputFile = arg;
            }
        }

        if (string.IsNullOrEmpty(outputFile))
        {
             Console.Error.WriteLine("Error: No output file specified.");
             PrintHelp();
             return 1;
        }

        try 
        {
            // Generate the stub license file
            var sb = new StringBuilder();
            sb.AppendLine("--- SourceGuardian License Stub ---");
            sb.AppendLine($"Generated: {DateTime.Now}");
            sb.AppendLine($"Output File: {outputFile}");
            sb.AppendLine();
            sb.AppendLine("--- Arguments ---");
            
            foreach(var kvp in parameters)
            {
                foreach(var val in kvp.Value)
                {
                    sb.AppendLine($"{kvp.Key}: {val}");
                    
                    // Special handling for --text @file to show content
                    if (kvp.Key == "--text" && val.StartsWith("@"))
                    {
                        string path = val.Substring(1);
                        sb.AppendLine($"  >>> Content of {path}:");
                        if (File.Exists(path))
                        {
                            try 
                            {
                                string content = File.ReadAllText(path);
                                sb.AppendLine(content);
                            }
                            catch(Exception ex)
                            {
                                sb.AppendLine($"  [Error reading file: {ex.Message}]");
                            }
                        }
                        else
                        {
                            sb.AppendLine($"  [File not found]");
                        }
                        sb.AppendLine("  <<< End Content");
                    }
                }
            }

            // Ensure directory exists
            var dir = Path.GetDirectoryName(outputFile);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.WriteAllText(outputFile, sb.ToString());
            Console.WriteLine($"License file generated successfully: {outputFile}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error writing output file: {ex.Message}");
            return 1;
        }

        if (parameters.ContainsKey("-w"))
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        return 0;
    }

    static void PrintHelp()
    {
        Console.WriteLine(@"SourceGuardian PRO 17.0.0 Script License Generator (Dec 11 2025 18:09:31)
MOCK VERSION - NOT REAL SOURCEGUARDIAN

Usage: licgen [options] output.lic

  --expire <dd/mm/yyyy>             Set script expiration date
  --expire <00d[00h[00m[00s]]]>     Set script expiration time from now
  --domain <domain>                 Bind script to domain name
  --domain-ignore-cli               Ignore domain name check for CLI
  --ip <x.x.x.x[/y.y.y.y]>          Bind script to ip/mask
  --ip-ignore-cli                   Ignore ip check for CLI
  --mac <xx:xx:xx:xx:xx:xx>         Bind script to mac address
  --machine-id <machine id>         Bind script to a machine id (read manual)
  --projid <value>                  Set project id (required, the same as for encoding)
  --projkey <value>                 Set project key (required, the same as for encoding)
  --const name=value                Set custom defined constant
  --time-server <server,server,...> Set time server (for expiration date check)
  --text ""text""|@file               Add plain text into the license file

  -w                  Wait for key press before exit
  -v                  Display version number
  --docker-socket <>  Path to the mapped docker socket for installing to a Docker container
  --license-file  <>  Path to the license file
  --license-release   Release the license for this installation
  --license-dynamic   PRO Dynamic license request (read manual)
  --license-username  License request username
  --license-password  License request password
  --license           Display license information
  -h                  Display this help");
    }
}