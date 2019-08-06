using Torun.Classes;
namespace Torun.Lang
{
    public static class CurrentLanguage
    {
        public static ILanguage Language { get; set; }
        public static void SetLanguage(TorunLanguage torunLanguage)
        {
            switch (torunLanguage)
            {
                default:
                case TorunLanguage.TR: Language = new TR();
                    break;

                case TorunLanguage.EN:
                    Language = new EN();
                    break;
            }
        }
    }
}
