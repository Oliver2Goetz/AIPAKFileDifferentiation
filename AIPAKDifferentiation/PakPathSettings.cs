using System;
using System.IO;
using System.Text.Json;

namespace AIPAKDifferentiation {

    internal class PakPathSettings {

        public bool RememberPaths { get; set; }
        public string Pak1Path { get; set; } = "";
        public string Pak2Path { get; set; } = "";

        public static string FilePath {
            get {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "AIPAKDifferentiation",
                    "settings.json"
                );
            }
        }

        public static PakPathSettings load(string filePath = null) {
            string settingsFilePath = filePath ?? FilePath;
            if (!File.Exists(settingsFilePath)) {
                return new PakPathSettings();
            }

            return JsonSerializer.Deserialize<PakPathSettings>(File.ReadAllText(settingsFilePath)) ?? new PakPathSettings();
        }

        public void save(string filePath = null) {
            string settingsFilePath = filePath ?? FilePath;
            string directory = Path.GetDirectoryName(settingsFilePath);
            Directory.CreateDirectory(directory);
            string temporaryPath = settingsFilePath + ".tmp";

            try {
                File.WriteAllText(temporaryPath, JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true }));
                File.Move(temporaryPath, settingsFilePath, true);
            } finally {
                if (File.Exists(temporaryPath)) {
                    File.Delete(temporaryPath);
                }
            }
        }
    }
}
