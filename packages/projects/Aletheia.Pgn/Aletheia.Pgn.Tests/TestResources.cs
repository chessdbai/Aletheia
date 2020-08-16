namespace Aletheia.Pgn.Tests
{
    using System;
    using System.IO;
    using System.Reflection;

    public class TestResources
    {
        private static Type MyType = typeof(TestResources);
        private static Assembly MyAssembly = MyType.Assembly;
        private static string MyNamespace = MyType.Namespace;
        private static string ResourceNamespace = $"{MyNamespace}.ExampleGames";

        public static string JohnDavisBeatsSieica => LoadResourceText(nameof(JohnDavisBeatsSieica));
        public static string GameWithIllegalNullMoveIndicator => LoadResourceText(nameof(GameWithIllegalNullMoveIndicator));
        public static string ShortLegalNullMoveGame => LoadResourceText(nameof(ShortLegalNullMoveGame));
        public static string GameWithNullComment => LoadResourceText(nameof(GameWithNullComment));
        public static string GameWithCuratorComment => LoadResourceText(nameof(GameWithCuratorComment));

        private static string LoadResourceText(string resource)
        {
            string fullName = $"{ResourceNamespace}.{resource}.pgn";
            using var stream = MyAssembly.GetManifestResourceStream(fullName);
            using var reader = new StreamReader(stream!);
            return reader.ReadToEnd();
        }
    }
}