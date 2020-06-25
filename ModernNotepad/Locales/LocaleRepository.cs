using System.Collections.Generic;
using System.Reflection;

namespace ModernNotepad.Locales
{
    class LocaleRepository
    {   
        public LocaleRepository()
        {
            Locales = new HashSet<string>
            {
                "en-US",
                "es-ES",
            };
            EnglishDictionary = new Dictionary<string, string>
            {
                { "OpenFileTitle", "Open File" },
                { "SaveFileTitle", "Save File" },
                { "FilterTxt", "Plain Text Files" },
                { "FilterAll", "All Files" },
                { "ConfirmationQuestion", "Would you like to save the changes made in the file?" },
                { "AppDescription", EnglishDescription },
                { "AppTitle", "Untitled" },
                { "NewDocument", "New Document.txt" },
                { "ErrorMessage", "Error executing the program. Please send a copy of the file \"error\" to the email: maxdr.mat@gmail.com"},
            };
            SpanishDictionary = new Dictionary<string, string>
            {
                { "OpenFileTitle", "Abrir archivo" },
                { "SaveFileTitle", "Guardar archivo" },
                { "FilterTxt", "Documento de texto" },
                { "FilterAll", "Todos los archivos" },
                { "ConfirmationQuestion", "¿Quieres guardar los cambios hechos en el archivo?" },
                { "AppDescription", SpanishDescription },
                { "AppTitle", "Sín título" },
                { "NewDocument", "Nuevo documento.txt" },
                { "ErrorMessage", "Error al ejecutar el programa. Por favor envíe una copia del archivo \"error\" al correo: maxdr.mat@gmail.com"},
            };
            Dictionaries = new Dictionary<string, Dictionary<string, string>>
            {
                { "en-US", EnglishDictionary },
                { "es-ES", SpanishDictionary },
            };
        }

        public ISet<string> Locales { get; }

        public Dictionary<string, string> EnglishDictionary { get; }

        public Dictionary<string, string> SpanishDictionary { get; }

        public Dictionary<string, Dictionary<string, string>> Dictionaries { get; }

        private string EnglishDescription
        {
            get
            {
                var attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        private string SpanishDescription
        {
            get
            {
                var description = string.Join(" ", new string[]
                {
                    "Modern Notepad es una aplicación de bloc de notas moderna y minimalista.",
                    "Está desarrollada con WPF en .Net Core 3.1.",
                });
                return description;
            }
        }
    }
}
