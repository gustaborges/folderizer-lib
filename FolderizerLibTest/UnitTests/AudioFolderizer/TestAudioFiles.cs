namespace FolderizerLibTest.UnitTests
{
    class TestAudioFiles
    {
        // Albums
        private static string tuReinas = "Tu Reinas";
        private static string somDaCura = "Som da Cura";
        private static string terremoto = "Terremoto";
        private static string maisDoce = "Mais Doce que o Mel";
        // Artists
        private static string eyshila = "Eyshila";
        private static string dianteDoTrono = "Diante do Trono";
        private static string bianca = "Bianca Toledo";
        // Genres
        private static string religious = "Religiosa";

        public static AudioFile[] Files =
        {
            new AudioFile {
                Name = "Ana Paula Valadão - Santo",
                Album = tuReinas,
                AlbumArtist = dianteDoTrono,
                Genre = religious,
                Year = "2014"
            },
            new AudioFile {
                Name = "Ana Paula Valadão - Rei dos Reis _ Cordeiro e Leão",
                Album = tuReinas,
                AlbumArtist = dianteDoTrono,
                Genre = religious,
                Year = "2014"
            },

            new AudioFile {
                Name = "Bianca Toledo - Na Dor",
                Album = somDaCura,
                AlbumArtist = bianca,
                Genre = religious,
                Year = "2014"
            },

            new AudioFile {
                Name = "Eyshila - A Prova do Que Não se Vê",
                Album = maisDoce,
                AlbumArtist = eyshila,
                Genre = religious,
                Year = "1999"
            },

            new AudioFile {
                Name = "Eyshila - Adorai ao Rei",
                Album = maisDoce,
                AlbumArtist = eyshila,
                Genre = religious,
                Year = "1999"
            },

            new AudioFile {
                Name = "Eyshila - Canção Para o Meu Filho",
                Album = maisDoce,
                AlbumArtist = eyshila,
                Genre = religious,
                Year = "1999"
            },

            new AudioFile {
                Name = "Eyshila - Chegar a Ti",
                Album = terremoto,
                AlbumArtist = eyshila,
                Genre = religious,
                Year = "2005"
            },

            new AudioFile {
                Name = "Eyshila - Chuva de Poder",
                Album = terremoto,
                AlbumArtist = eyshila,
                Genre = religious,
                Year = "2005"
            }
        };
    }

    class AudioFile
    {
        public string Name { get; set; }
        public string Album { get; set; }
        public string AlbumArtist { get; set; }
        public string Year { get; set; }
        public string Genre { get; set; }
        public string Format { get; set; } = ".mp3";
    }
}
