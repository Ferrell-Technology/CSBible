using System;
using System.Collections.Generic;
using System.Linq;

namespace CSBible
{
    /// <summary>
    /// Class for Bible actions.
    /// </summary>
    /// <exception cref="ChapterOutOfRangeException"></exception>
    /// <exception cref="VerseOutOfRangeException"></exception>
    public class Bible
    {
        private static List<string> lines = new List<string>();
        private static List<string> verses = new List<string>();
        private static List<Verse> search = new List<Verse>();
        private static bool init = false;
        
        /// <summary>
        /// Starts an instance of the Bible class.
        /// </summary>
        public Bible()
        {
            foreach (string line in Resources.kjvdat.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                lines.Add(line);
            }
            init = true;
        }

        /// <summary>
        /// Returns a verse based on the specified book, chapter, and verse.
        /// </summary>
        /// <param name="book">The book to get the chapter from.</param>
        /// <param name="chapter">The chapter to get the verse from.</param>
        /// <param name="verse">The verse to return.</param>
        /// <returns></returns>
        /// <exception cref="ChapterOutOfRangeException"></exception>
        /// <exception cref="VerseOutOfRangeException"></exception>
        public string GetVerse(Book book, int chapter, int verse)
        {
            //Index of selected book:
            int i = (int)book;
            Abrevations c = (Abrevations)i;
            //Abrevation of selected book:
            string abrev = c.ToString();
            int chapterLimit = (int)Enum.Parse(typeof(Chaps), book.ToString());
            int verseLimit = Indexes.GetVerseLimit(book, chapter);
            if (chapter <= chapterLimit && verseLimit != 0)
            {
                if (verse <= verseLimit && verse > 0)
                {
                    foreach (string line in lines)
                    {
                        if (line.StartsWith(abrev + "|" + chapter.ToString() + "|" + verse.ToString() + "|"))
                        {
                            return line.Split('|')[3].TrimStart(' ').TrimEnd('~');
                        }
                    }
                }
                else
                {
                    throw new VerseOutOfRangeException("The specified verse is out of range of valid values.");
                }
            }
            else
            {
                throw new ChapterOutOfRangeException("The specified chapter is out of range of valid values.");
            }

            return null;
        }

        /// <summary>
        /// Returns a verse based on the parameters specified in a VerseLocation object.
        /// </summary>
        /// <param name="location">The VerseLocation instance for verse retrieval.</param>
        /// <returns></returns>
        /// <exception cref="ChapterOutOfRangeException"></exception>
        /// <exception cref="VerseOutOfRangeException"></exception>
        public string GetVerse(VerseLocation location)
        {
            //Index of selected book:
            int i = (int)location.Book;
            Abrevations c = (Abrevations)i;
            //Abrevation of selected book:
            string abrev = c.ToString();
            int chapterLimit = (int)Enum.Parse(typeof(Chaps), location.Book.ToString());
            int verseLimit = Indexes.GetVerseLimit(location.Book, location.Chapter);
            if (location.Chapter <= chapterLimit && verseLimit != 0)
            {
                if (location.Verse <= verseLimit && location.Verse > 0)
                {
                    foreach (string line in lines)
                    {
                        if (line.StartsWith(abrev + "|" + location.Chapter.ToString() + "|" + location.Verse.ToString() + "|"))
                        {
                            return line.Split('|')[3].TrimStart(' ').TrimEnd('~');
                        }
                    }
                }
                else
                {
                    throw new VerseOutOfRangeException("The specified verse is out of range of valid values.");
                }
            }
            else
            {
                throw new ChapterOutOfRangeException("The specified chapter is out of range of valid values.");
            }

            return null;
        }

        /// <summary>
        /// Returns a string array of verses that compose the chapter in the given book. The resulting string array is not zero-based (index 1 is verse 1, index 2 is verse 2, etc.).
        /// </summary>
        /// <param name="book">The book to get the chapter from.</param>
        /// <param name="chapter">The chapter to get the verses from.</param>
        /// <returns></returns>
        /// <exception cref="ChapterOutOfRangeException"></exception>
        public string[] GetChapter(Book book, int chapter)
        {
            verses.Clear();
            //Index of selected book:
            int i = (int)book;
            Abrevations c = (Abrevations)i;
            //Abrevation of selected book:
            string abrev = c.ToString();
            verses.Add("");

            int chapterLimit = (int)Enum.Parse(typeof(Chaps), book.ToString());
            int verseLimit = Indexes.GetVerseLimit(book, chapter);
            if (chapter <= chapterLimit && verseLimit != 0)
            {
                foreach (string line in lines)
                {
                    if (line.StartsWith(abrev + "|" + chapter + "|"))
                    {
                        verses.Add(line.Split('|')[3].TrimStart(' ').TrimEnd('~'));
                    }
                }
            }
            else
            {
                throw new ChapterOutOfRangeException("The specified chapter is out of range of valid values.");
            }
            return verses.ToArray();
        }

        /// <summary>
        /// Returns a string array of verses that compose the chapter in the given book.
        /// </summary>
        /// <param name="book">The book to get the chapter from.</param>
        /// <param name="chapter">The chapter to get the verses from.</param>
        /// <param name="zeroBased">Determines whether the returned string array should be zero-based.</param>
        /// <returns></returns>
        /// <exception cref="ChapterOutOfRangeException"></exception>
        public string[] GetChapter(Book book, int chapter, bool zeroBased)
        {
            verses.Clear();
            //Index of selected book:
            int i = (int)book;
            Abrevations c = (Abrevations)i;
            //Abrevation of selected book:
            string abrev = c.ToString();
            if (zeroBased == false)
            {
                verses.Add("");
            }
            int chapterLimit = (int)Enum.Parse(typeof(Chaps), book.ToString());
            int verseLimit = Indexes.GetVerseLimit(book, chapter);
            if (chapter <= chapterLimit && verseLimit != 0)
            {
                foreach (string line in lines)
                {
                    if (line.StartsWith(abrev + "|" + chapter + "|"))
                    {
                        verses.Add(line.Split('|')[3].TrimStart(' ').TrimEnd('~'));
                    }
                }
            }
            else
            {
                throw new ChapterOutOfRangeException("The specified chapter is out of range of valid values.");
            }
            return verses.ToArray();
        }

        /// <summary>
        /// Returns a string array of verses the comprise the specified chapter from a ChapterLocation object.
        /// </summary>
        /// <param name="location">The ChapterLocation instance for chapter retrieval.</param>
        /// <returns></returns>
        /// <exception cref="ChapterOutOfRangeException"></exception>
        public string[] GetChapter(ChapterLocation location)
        {
            verses.Clear();
            //Index of selected book:
            int i = (int)location.Book;
            Abrevations c = (Abrevations)i;
            //Abrevation of selected book:
            string abrev = c.ToString();
            verses.Add("");
            int chapterLimit = (int)Enum.Parse(typeof(Chaps), location.Book.ToString());
            int verseLimit = Indexes.GetVerseLimit(location.Book, location.Chapter);
            if (location.Chapter <= chapterLimit && verseLimit != 0)
            {
                foreach (string line in lines)
                {
                    if (line.StartsWith(abrev + "|" + location.Chapter + "|"))
                    {
                        verses.Add(line.Split('|')[3].TrimStart(' ').TrimEnd('~'));
                    }
                }
            }
            else
            {
                throw new ChapterOutOfRangeException("The specified chapter is out of range of valid values.");
            }
            return verses.ToArray();
        }

        /// <summary>
        /// Returns a list of verses that compose the chapter from the given book. The resulting string list is not zero-based (index 1 is verse 1, index 2 is verse 2, etc.).
        /// </summary>
        /// <param name="book">The book to get the chapter from.</param>
        /// <param name="chapter">The chapter to get the verses from.</param>
        /// <returns></returns>
        /// /// <exception cref="ChapterOutOfRangeException"></exception>
        public List<string> GetChapterAsList(Book book, int chapter)
        {
            verses.Clear();
            //Index of selected book:
            int i = (int)book;
            Abrevations c = (Abrevations)i;
            //Abrevation of selected book:
            string abrev = c.ToString();
            verses.Add("");
            int chapterLimit = (int)Enum.Parse(typeof(Chaps), book.ToString());
            int verseLimit = Indexes.GetVerseLimit(book, chapter);
            if (chapter <= chapterLimit && verseLimit != 0)
            {
                foreach (string line in lines)
                {
                    if (line.StartsWith(abrev + "|" + chapter + "|"))
                    {
                        verses.Add(line.Split('|')[3].TrimStart(' ').TrimEnd('~'));
                    }
                }
            }
            else
            {
                throw new ChapterOutOfRangeException("The specified chapter is out of range of valid values.");
            }
            return verses;
        }

        /// <summary>
        /// Returns a list of verses that compose the chapter from the given book.
        /// </summary>
        /// <param name="book">The book to get the chapter from.</param>
        /// <param name="chapter">The chapter to get the verses from.</param>
        /// <param name="zeroBased">Determines whether the returned string list should be zero-based.</param>
        /// <returns></returns>
        /// <exception cref="ChapterOutOfRangeException"></exception>
        public List<string> GetChapterAsList(Book book, int chapter, bool zeroBased)
        {
            verses.Clear();
            //Index of selected book:
            int i = (int)book;
            Abrevations c = (Abrevations)i;
            //Abrevation of selected book:
            string abrev = c.ToString();
            if (zeroBased == false)
            {
                verses.Add("");
            }
            int chapterLimit = (int)Enum.Parse(typeof(Chaps), book.ToString());
            int verseLimit = Indexes.GetVerseLimit(book, chapter);
            if (chapter <= chapterLimit && verseLimit != 0)
            {
                foreach (string line in lines)
                {
                    if (line.StartsWith(abrev + "|" + chapter + "|"))
                    {
                        verses.Add(line.Split('|')[3].TrimStart(' ').TrimEnd('~'));
                    }
                }
            }
            else
            {
                throw new ChapterOutOfRangeException("The specified chapter is out of range of valid values.");
            }
            return verses;
        }

        /// <summary>
        /// Returns a string list of verses the comprise the specified chapter from a ChapterLocation object.
        /// </summary>
        /// <param name="location">The ChapterLocation instance for chapter retrieval.</param>
        /// <returns></returns>
        /// <exception cref="ChapterOutOfRangeException"></exception>
        public List<string> GetChapterAsList(ChapterLocation location)
        {
            verses.Clear();
            //Index of selected book:
            int i = (int)location.Book;
            Abrevations c = (Abrevations)i;
            //Abrevation of selected book:
            string abrev = c.ToString();
            verses.Add("");
            int chapterLimit = (int)Enum.Parse(typeof(Chaps), location.Book.ToString());
            int verseLimit = Indexes.GetVerseLimit(location.Book, location.Chapter);
            if (location.Chapter <= chapterLimit && verseLimit != 0)
            {
                foreach (string line in lines)
                {
                    if (line.StartsWith(abrev + "|" + location.Chapter + "|"))
                    {
                        verses.Add(line.Split('|')[3].TrimStart(' ').TrimEnd('~'));
                    }
                }
            }
            else
            {
                throw new ChapterOutOfRangeException("The specified chapter is out of range of valid values.");
            }
            return verses;
        }

        /// <summary>
        /// Searches the entire Bible for the specified word or phrase and returns a string list of verses that contain it.
        /// </summary>
        /// <param name="query">The word or phrase to search.</param>
        /// <returns></returns>
        public List<string> Find(string query)
        {
            verses.Clear();
            foreach (string line in lines)
            {
                if (line.Contains(query))
                {
                    verses.Add(line.Split('|')[3].TrimStart(' ').TrimEnd('~'));
                }
            }
            return verses;
        }

        /// <summary>
        /// Searches in the specified area of the Bible for verses that contain a given word or phrase and returns a string list of verses that contain it.
        /// </summary>
        /// <param name="query">The word or phrase to search.</param>
        /// <param name="filter">The category of the Bible to search in.</param>
        /// <returns></returns>
        public List<string> Find(string query, Filter filter)
        {
            verses.Clear();
            string[] booksToSearch = Indexes.FilterKeys[filter.ToString()].Split('-');
            foreach (string line in lines)
            {
                if (booksToSearch.Any(line.StartsWith) && line.Contains(query))
                {
                    verses.Add(line.Split('|')[3].TrimStart(' ').TrimEnd('~'));
                }
            }
            return verses;
        }

        /// <summary>
        /// Searches in the specified book of the Bible for verses that contain a given word or phrase and returns a string list of verses that contain it.
        /// </summary>
        /// <param name="query">The word or phrase to search.</param>
        /// <param name="book">The book of the Bible to search in.</param>
        /// <returns></returns>
        public List<string> Find(string query, Book book)
        {
            verses.Clear();
            foreach (string line in lines)
            {
                if (line.StartsWith(Indexes.TranslateAbrevation(book)) && line.Contains(query))
                {
                    verses.Add(line.Split('|')[3].TrimStart(' ').TrimEnd('~'));
                }
            }
            return verses;
        }

        /// <summary>
        /// Searches the entire Bible for a given word or phrase, and returns a list of Verse objects containing the book, chapter, and verse that the word or phrase was found in.
        /// </summary>
        /// <param name="query">The string to search.</param>
        /// <returns></returns>
        public List<Verse> Search(string query)
        {
            search.Clear();
            foreach (string line in lines)
            {
                if (line.Contains(query))
                {          
                    search.Add(new Verse(new VerseLocation((Book)Enum.Parse(typeof(Book), Indexes.TranslateBook(line.Split('|')[0])), Convert.ToInt32(line.Split('|')[1]), Convert.ToInt32(line.Split('|')[2])), line.Split('|')[3].TrimStart(' ').TrimEnd('~')));
                }
            }
            return search;
        }

        /// <summary>
        /// Searches the specified area of the Bible for a given word or phrase, and returns a list of Verse objects containing the book, chapter, and verse that the word or phrase was found in.
        /// </summary>
        /// <param name="query">The string to search.</param>
        /// <param name="filter">the category of the Bible to search in.</param>
        /// <returns></returns>
        public List<Verse> Search(string query, Filter filter)
        {
            search.Clear();
            string[] booksToSearch = Indexes.FilterKeys[filter.ToString()].Split('-');
            foreach (string line in lines)
            {
                if (booksToSearch.Any(line.StartsWith) && line.Contains(query))
                {
                    search.Add(new Verse(new VerseLocation((Book)Enum.Parse(typeof(Book), Indexes.TranslateBook(line.Split('|')[0])), Convert.ToInt32(line.Split('|')[1]), Convert.ToInt32(line.Split('|')[2])), line.Split('|')[3].TrimStart(' ').TrimEnd('~')));
                }
            }
            return search;
        }

        /// <summary>
        /// Searches in the specified book of the Bible for a given word or phrase, and returns a list of Verse objects containing the book, chapter, and verse that the word or phrase was found in.
        /// </summary>
        /// <param name="query">The string to search.</param>
        /// <param name="book">The book of the Bible to search in.</param>
        /// <returns></returns>
        public List<Verse> Search(string query, Book book)
        {
            search.Clear();
            foreach (string line in lines)
            {
                if (line.StartsWith(Indexes.TranslateAbrevation(book)) && line.Contains(query))
                {
                    search.Add(new Verse(new VerseLocation((Book)Enum.Parse(typeof(Book), Indexes.TranslateBook(line.Split('|')[0])), Convert.ToInt32(line.Split('|')[1]), Convert.ToInt32(line.Split('|')[2])), line.Split('|')[3].TrimStart(' ').TrimEnd('~')));
                }
            }
            return search;
        }

        /// <summary>
        /// Filters for Bible searches.
        /// </summary>
        public enum Filter
            {
                /// <summary>
                /// The New Testament (Matthew--Revelation)
                /// </summary>
                New_Testament,
                /// <summary>
                /// The Old Testament (Genesis--Malachi)
                /// </summary>
                Old_Testament,
                /// <summary>
                /// The Pentateuch (Genesis--Deuteronomy)
                /// </summary>
                Pentateuch,
                /// <summary>
                /// The Historical Books (Joshua--Esther)
                /// </summary>
                Historical_Books,
                /// <summary>
                /// The Poetical and Wisdom Books (Job--Song of Solomon and Lamentations)
                /// </summary>
                Poetical_and_Wisdom_Books,
                /// <summary>
                /// The Prophets (Isaiah, Jeremiah, Ezekiel--Malachi)
                /// </summary>
                The_Prophets,
                /// <summary>
                /// The Gospels (Matthew--John)
                /// </summary>
                The_Gospels,
                /// <summary>
                /// The Acts (Acts)
                /// </summary>
                The_Acts,
                /// <summary>
                /// Paul's Letters (Romans--Philemon)
                /// </summary>
                Pauls_Letters,
                /// <summary>
                /// General Epistles and Revelation (Hebrews--Revelation)
                /// </summary>
                General_Epistles_and_Revelation
            }

        /// <summary>
        /// Closes the current Bible instance.
        /// </summary>
        public void Close()
        {
            if (init == true)
            {
                lines.Clear();
                verses.Clear();
                search.Clear();
                init = false;
            }
            else
            {
                throw new Exception("Class instance cannot be closed because it hasn't been opened.");
            }
        }

        internal enum Chaps
        {
            Genesis = 50,
            Exodus = 40,
            Leviticus = 27,
            Numbers = 36,
            Deuteronomy = 34,
            Joshua = 24,
            Judges = 21,
            Ruth = 4,
            First_Samuel = 31,
            Second_Samuel = 24,
            First_Kings = 22,
            Second_Kings = 25,
            First_Chronicles = 29,
            Second_Chronicles = 36,
            Ezra = 10,
            Nehemiah = 13,
            Esther = 10,
            Job = 42,
            Psalms = 150,
            Proverbs = 31,
            Ecclesiastes = 12,
            SongofSolomon = 8,
            Isaiah = 66,
            Jeremiah = 52,
            Lamentations = 5,
            Ezekiel = 48,
            Daniel = 12,
            Hosea = 14,
            Joel = 3,
            Amos = 9,
            Obadiah = 1,
            Jonah = 4,
            Micah = 7,
            Nahum = 3,
            Habakkuk = 3,
            Zephaniah = 3,
            Haggai = 2,
            Zechariah = 14,
            Malachi = 4,
            Matthew = 28,
            Mark = 16,
            Luke = 24,
            John = 21,
            Acts = 28,
            Romans = 16,
            First_Corinthians = 16,
            Second_Corinthians = 13,
            Galatians = 6,
            Ephesians = 6,
            Philippians = 4,
            Colossians = 4,
            First_Thessalonians = 5,
            Second_Thessalonians = 3,
            First_Timothy = 6,
            Second_Timothy = 4,
            Titus = 3,
            Philemon = 1,
            Hebrews = 13,
            James = 5,
            First_Peter = 5,
            Second_Peter = 3,
            First_John = 5,
            Second_John = 1,
            Third_John = 1,
            Jude = 1,
            Revelation = 22
        }

        internal enum Abrevations
        {
            Gen,
            Exo,
            Lev,
            Num,
            Deu,
            Jos,
            Jdg,
            Rut,
            Sa1,
            Sa2,
            Kg1,
            Kg2,
            Ch1,
            Ch2,
            Ezr,
            Neh,
            Est,
            Job,
            Psa,
            Pro,
            Ecc,
            Sol,
            Isa,
            Jer,
            Lam,
            Eze,
            Dan,
            Hos,
            Joe,
            Amo,
            Oba,
            Jon,
            Mic,
            Nah,
            Hab,
            Zep,
            Hag,
            Zac,
            Mal,
            Mat,
            Mar,
            Luk,
            Joh,
            Act,
            Rom,
            Co1,
            Co2,
            Gal,
            Eph,
            Phi,
            Col,
            Th1,
            Th2,
            Ti1,
            Ti2,
            Tit,
            Phm,
            Heb,
            Jam,
            Pe1,
            Pe2,
            Jo1,
            Jo2,
            Jo3,
            Jde,
            Rev
        }
    }

    /// <summary>
    /// Books of the Bible.
    /// </summary>
    public enum Book
    {
        /// <summary>
        /// The book of Genesis.
        /// </summary>
        Genesis,
        /// <summary>
        /// The book of Exodus.
        /// </summary>
        Exodus,
        /// <summary>
        /// The book of Leviticus.
        /// </summary>
        Leviticus,
        /// <summary>
        /// The book of Numbers.
        /// </summary>
        Numbers,
        /// <summary>
        /// The book of Deuteronomy.
        /// </summary>
        Deuteronomy,
        /// <summary>
        /// The book of Joshua.
        /// </summary>
        Joshua,
        /// <summary>
        /// The book of Judges.
        /// </summary>
        Judges,
        /// <summary>
        /// The book of Ruth.
        /// </summary>
        Ruth,
        /// <summary>
        /// The book of 1 Samuel.
        /// </summary>
        First_Samuel,
        /// <summary>
        /// The book of 2 Samuel.
        /// </summary>
        Second_Samuel,
        /// <summary>
        /// The book of 1 Kings.
        /// </summary>
        First_Kings,
        /// <summary>
        /// The book of 2 Kings.
        /// </summary>
        Second_Kings,
        /// <summary>
        /// The book of 1 Chronicles.
        /// </summary>
        First_Chronicles,
        /// <summary>
        /// The book of 2 Chronicles.
        /// </summary>
        Second_Chronicles,
        /// <summary>
        /// The book of Ezra.
        /// </summary>
        Ezra,
        /// <summary>
        /// The book of Nehemiah.
        /// </summary>
        Nehemiah,
        /// <summary>
        /// The book of Esther.
        /// </summary>
        Esther,
        /// <summary>
        /// The book of Job.
        /// </summary>
        Job,
        /// <summary>
        /// The book of Psalms.
        /// </summary>
        Psalms,
        /// <summary>
        /// The book of Proverbs.
        /// </summary>
        Proverbs,
        /// <summary>
        /// The book of Ecclesiastes.
        /// </summary>
        Ecclesiastes,
        /// <summary>
        /// The book of Song of Solomon, or Song of Songs.
        /// </summary>
        SongofSolomon,
        /// <summary>
        /// The book of Isaiah.
        /// </summary>
        Isaiah,
        /// <summary>
        /// The book of Jeremiah.
        /// </summary>
        Jeremiah,
        /// <summary>
        /// The book of Lamentations.
        /// </summary>
        Lamentations,
        /// <summary>
        /// The book of Ezekiel.
        /// </summary>
        Ezekiel,
        /// <summary>
        /// The book of Daniel.
        /// </summary>
        Daniel,
        /// <summary>
        /// The book of Hosea.
        /// </summary>
        Hosea,
        /// <summary>
        /// The book of Joel.
        /// </summary>
        Joel,
        /// <summary>
        /// The book of Amos.
        /// </summary>
        Amos,
        /// <summary>
        /// The book of Obadiah.
        /// </summary>
        Obadiah,
        /// <summary>
        /// The book of Jonah.
        /// </summary>
        Jonah,
        /// <summary>
        /// The book of Micah.
        /// </summary>
        Micah,
        /// <summary>
        /// The book of Nahum.
        /// </summary>
        Nahum,
        /// <summary>
        /// The book of Habakkuk.
        /// </summary>
        Habakkuk,
        /// <summary>
        /// The book of Zephaniah.
        /// </summary>
        Zephaniah,
        /// <summary>
        /// The book of Haggai.
        /// </summary>
        Haggai,
        /// <summary>
        /// The book of Zechariah.
        /// </summary>
        Zechariah,
        /// <summary>
        /// The book of Malachi.
        /// </summary>
        Malachi,
        /// <summary>
        /// The book of Matthew.
        /// </summary>
        Matthew,
        /// <summary>
        /// The book of Mark.
        /// </summary>
        Mark,
        /// <summary>
        /// The book of Luke.
        /// </summary>
        Luke,
        /// <summary>
        /// The book of John.
        /// </summary>
        John,
        /// <summary>
        /// The book of Acts.
        /// </summary>
        Acts,
        /// <summary>
        /// The book of Romans.
        /// </summary>
        Romans,
        /// <summary>
        /// The book of 1 Corinthians.
        /// </summary>
        First_Corinthians,
        /// <summary>
        /// The book of 2 Corinthians.
        /// </summary>
        Second_Corinthians,
        /// <summary>
        /// The book of Galations.
        /// </summary>
        Galatians,
        /// <summary>
        /// The book of Ephesians.
        /// </summary>
        Ephesians,
        /// <summary>
        /// The book of Philippians.
        /// </summary>
        Philippians,
        /// <summary>
        /// The book of Colossians.
        /// </summary>
        Colossians,
        /// <summary>
        /// The book of 1 Thessalonians.
        /// </summary>
        First_Thessalonians,
        /// <summary>
        /// The book of 2 Thessalonians.
        /// </summary>
        Second_Thessalonians,
        /// <summary>
        /// The book of 1 Timothy.
        /// </summary>
        First_Timothy,
        /// <summary>
        /// The book of 2 Timothy.
        /// </summary>
        Second_Timothy,
        /// <summary>
        /// The book of Titus.
        /// </summary>
        Titus,
        /// <summary>
        /// The book of Philemon.
        /// </summary>
        Philemon,
        /// <summary>
        /// The book of Hebrews.
        /// </summary>
        Hebrews,
        /// <summary>
        /// The book of James.
        /// </summary>
        James,
        /// <summary>
        /// The book of 1 Peter.
        /// </summary>
        First_Peter,
        /// <summary>
        /// The book of 2 Peter.
        /// </summary>
        Second_Peter,
        /// <summary>
        /// The book of 1 John.
        /// </summary>
        First_John,
        /// <summary>
        /// The book of 2 John.
        /// </summary>
        Second_John,
        /// <summary>
        /// The book of 3 John.
        /// </summary>
        Third_John,
        /// <summary>
        /// The book of Jude.
        /// </summary>
        Jude,
        /// <summary>
        /// The book of Revelation.
        /// </summary>
        Revelation
    }

    /// <summary>
    /// Represents the location of a Bible verse.
    /// </summary>
    public class VerseLocation
    {
        /// <summary>
        /// The book that contains the chapter and verse.
        /// </summary>
        public Book Book { get; private set; }
        /// <summary>
        /// The chapter that contains the verse.
        /// </summary>
        public int Chapter { get; private set; }
        /// <summary>
        /// The verse that comprises the location object.
        /// </summary>
        public int Verse { get; private set; }
        /// <summary>
        /// Starts a new instance of the VerseLocation class.
        /// </summary>
        /// <param name="book"></param>
        /// <param name="chapter"></param>
        /// <param name="verse"></param>
        public VerseLocation(Book book, int chapter, int verse)
        {
            Book = book;
            Chapter = chapter;
            Verse = verse;
        }
    }

    /// <summary>
    /// Represents the location of a Bible verse and the verse itself.
    /// </summary>
    public class Verse
    {
        /// <summary>
        /// The location of the verse.
        /// </summary>
        public VerseLocation Location { get; private set; }
        /// <summary>
        /// The text content of the verse.
        /// </summary>
        public string Text { get; private set; }
        /// <summary>
        /// Starts a new instance of the Verse class.
        /// </summary>
        /// <param name="location">The location of the verse.</param>
        /// <param name="text">The text content of the verse.</param>
        public Verse(VerseLocation location, string text)
        {
            Location = location;
            Text = text;
        }
    }

    /// <summary>
    /// Represents the location of a Bible chapter.
    /// </summary>
    public class ChapterLocation
    {
        /// <summary>
        /// The book that contains the chapter.
        /// </summary>
        public Book Book { get; private set; }
        /// <summary>
        /// The chapter that comprises the location object.
        /// </summary>
        public int Chapter { get; private set; }
        /// <summary>
        /// Starts a new instance of the ChapterLocation class.
        /// </summary>
        /// <param name="book"></param>
        /// <param name="chapter"></param>
        public ChapterLocation(Book book, int chapter)
        {
            Book = book;
            Chapter = chapter;
        }
    }

    internal class Indexes
    {
        #region VerseIndexes
        internal static List<string> Gen = new List<string>()
        {
            "C1-31", "C2-25", "C3-24", "C4-26", "C5-32",
            "C6-22", "C7-24", "C8-22", "C9-29", "C10-32",
            "C11-32", "C12-20", "C13-18", "C14-24", "C15-21",
            "C16-16", "C17-27", "C18-33", "C19-38", "C20-18",
            "C21-34", "C22-24", "C23-20", "C24-67", "C25-34",
            "C26-35", "C27-46", "C28-22", "C29-35", "C30-43",
            "C31-55", "C32-32", "C33-20", "C34-31", "C35-29",
            "C36-43", "C37-36", "C38-30", "C39-23", "C40-23",
            "C41-57", "C42-38", "C43-34", "C44-34", "C45-28",
            "C46-34", "C47-31", "C48-22", "C49-33", "C50-26"
        };
        internal static List<string> Exo = new List<string>()
        {
            "C1-22", "C2-25", "C3-22", "C4-31", "C5-23",
            "C6-30", "C7-25", "C8-32", "C9-35", "C10-29",
            "C11-10", "C12-51", "C13-22", "C14-31", "C15-27",
            "C16-36", "C17-16", "C18-27", "C19-25", "C20-26",
            "C21-36", "C22-31", "C23-33", "C24-18", "C25-40",
            "C26-37", "C27-21", "C28-43", "C29-46", "C30-38",
            "C31-18", "C32-35", "C33-23", "C34-35", "C35-35",
            "C36-38", "C37-29", "C38-31", "C39-43", "C40-38"
        };
        internal static List<string> Lev = new List<string>()
        {
            "C1-17", "C2-16", "C3-17", "C4-35", "C5-19",
            "C6-30", "C7-38", "C8-36", "C9-24", "C10-20",
            "C11-47", "C12-8", "C13-59", "C14-57", "C15-33",
            "C16-34", "C17-16", "C18-30", "C19-37", "C20-27",
            "C21-24", "C22-33", "C23-44", "C24-23", "C25-55",
            "C26-46", "C27-34"
        };
        internal static List<string> Num = new List<string>()
        {
            "C1-54", "C2-34", "C3-51", "C4-49", "C5-31",
            "C6-27", "C7-89", "C8-26", "C9-23", "C10-36",
            "C11-35", "C12-16", "C13-33", "C14-45", "C15-41",
            "C16-50", "C17-13", "C18-32", "C19-22", "C20-29",
            "C21-35", "C22-41", "C23-30", "C24-25", "C25-18",
            "C26-65", "C27-23", "C28-31", "C29-40", "C30-16",
            "C31-54", "C32-42", "C33-56", "C34-29", "C35-34",
            "C36-13"
        };
        internal static List<string> Deu = new List<string>()
        {
            "C1-46", "C2-37", "C3-29", "C4-49", "C5-33",
            "C6-25", "C7-26", "C8-20", "C9-29", "C10-22",
            "C11-32", "C12-32", "C13-18", "C14-29", "C15-23",
            "C16-22", "C17-20", "C18-22", "C19-21", "C20-20",
            "C21-23", "C22-30", "C23-25", "C24-22", "C25-19",
            "C26-19", "C27-26", "C28-68", "C29-29", "C30-20",
            "C31-30", "C32-52", "C33-29", "C34-12"
        };
        internal static List<string> Jos = new List<string>()
        {
            "C1-18", "C2-24", "C3-17", "C4-24", "C5-15",
            "C6-27", "C7-26", "C8-35", "C9-27", "C10-43",
            "C11-23", "C12-24", "C13-33", "C14-15", "C15-63",
            "C16-10", "C17-18", "C18-28", "C19-51", "C20-9",
            "C21-45", "C22-34", "C23-16", "C24-33"
        };
        internal static List<string> Jdg = new List<string>()
        {
            "C1-36", "C2-23", "C3-31", "C4-24", "C5-31",
            "C6-40", "C7-25", "C8-35", "C9-57", "C10-18",
            "C11-40", "C12-15", "C13-25", "C14-20", "C15-20",
            "C16-31", "C17-13", "C18-31", "C19-30", "C20-48",
            "C21-25"
        };
        internal static List<string> Rut = new List<string>()
        {
            "C1-22", "C2-23", "C3-18", "C4-22"
        };
        internal static List<string> Sa1 = new List<string>()
        {
            "C1-28", "C2-36", "C3-21", "C4-22", "C5-12",
            "C6-21", "C7-17", "C8-22", "C9-27", "C10-27",
            "C11-15", "C12-25", "C13-23", "C14-52", "C15-35",
            "C16-23", "C17-58", "C18-30", "C19-24", "C20-42",
            "C21-15", "C22-23", "C23-29", "C24-22", "C25-44",
            "C26-25", "C27-12", "C28-25", "C29-11", "C30-31",
            "C31-13"
        };
        internal static List<string> Sa2 = new List<string>()
        {
            "C1-27", "C2-32", "C3-39", "C4-12", "C5-25",
            "C6-23", "C7-29", "C8-18", "C9-13", "C10-19",
            "C11-27", "C12-31", "C13-39", "C14-33", "C15-37",
            "C16-23", "C17-29", "C18-33", "C19-43", "C20-26",
            "C21-22", "C22-51", "C23-39", "C24-25"
        };
        internal static List<string> Kg1 = new List<string>()
        {
            "C1-53", "C2-46", "C3-28", "C4-34", "C5-18",
            "C6-38", "C7-51", "C8-66", "C9-28", "C10-29",
            "C11-43", "C12-33", "C13-34", "C14-31", "C15-34",
            "C16-34", "C17-24", "C18-46", "C19-21", "C20-43",
            "C21-29", "C22-53"
        };
        internal static List<string> Kg2 = new List<string>()
        {
            "C1-18", "C2-25", "C3-27", "C4-44", "C5-27",
            "C6-33", "C7-20", "C8-29", "C9-37", "C10-36",
            "C11-21", "C12-21", "C13-25", "C14-29", "C15-38",
            "C16-20", "C17-41", "C18-37", "C19-37", "C20-21",
            "C21-26", "C22-20", "C23-37", "C24-20", "C25-30"
        };
        internal static List<string> Ch1 = new List<string>()
        {
            "C1-54", "C2-55", "C3-24", "C4-43", "C5-26",
            "C6-81", "C7-40", "C8-40", "C9-44", "C10-14",
            "C11-47", "C12-40", "C13-14", "C14-17", "C15-29",
            "C16-43", "C17-27", "C18-17", "C19-19", "C20-8",
            "C21-30", "C22-19", "C23-32", "C24-31", "C25-31",
            "C26-32", "C27-34", "C28-21", "C29-30"
        };
        internal static List<string> Ch2 = new List<string>()
        {
            "C1-17", "C2-18", "C3-17", "C4-22", "C5-14",
            "C6-42", "C7-22", "C8-18", "C9-31", "C10-19",
            "C11-23", "C12-16", "C13-22", "C14-15", "C15-19",
            "C16-14", "C17-19", "C18-34", "C19-11", "C20-37",
            "C21-20", "C22-12", "C23-21", "C24-27", "C25-28",
            "C26-23", "C27-9", "C28-27", "C29-36", "C30-27",
            "C31-21", "C32-33", "C33-25", "C34-33", "C35-27",
            "C36-23"
        };
        internal static List<string> Ezr = new List<string>()
        {
            "C1-11", "C2-70", "C3-13", "C4-24", "C5-17",
            "C6-22", "C7-28", "C8-36", "C9-15", "C10-44"
        };
        internal static List<string> Neh = new List<string>()
        {
            "C1-11", "C2-20", "C3-32", "C4-23", "C5-19",
            "C6-19", "C7-73", "C8-18", "C9-38", "C10-39",
            "C11-36", "C12-47", "C13-31"
        };
        internal static List<string> Est = new List<string>()
        {
            "C1-22", "C2-23", "C3-15", "C4-17", "C5-14",
            "C6-14", "C7-10", "C8-17", "C9-32", "C10-3"
        };
        internal static List<string> Job = new List<string>()
        {
            "C1-22", "C2-13", "C3-26", "C4-21", "C5-27",
            "C6-30", "C7-21", "C8-22", "C9-35", "C10-22",
            "C11-20", "C12-25", "C13-28", "C14-22", "C15-35",
            "C16-16", "C17-16", "C18-21", "C19-29", "C20-29",
            "C21-34", "C22-30", "C23-17", "C24-25", "C25-6",
            "C26-14", "C27-23", "C28-28", "C29-25", "C30-31",
            "C31-40", "C32-22", "C33-33", "C34-37", "C35-16",
            "C36-33", "C37-24", "C38-41", "C39-30", "C40-24",
            "C41-34", "C42-17"
        };
        internal static List<string> Psa = new List<string>()
        {
            "C1-6", "C2-12", "C3-8", "C4-8", "C5-12",
            "C6-10", "C7-17", "C8-9", "C9-20", "C10-18",
            "C11-7", "C12-8", "C13-6", "C14-7", "C15-5",
            "C16-11", "C17-15", "C18-50", "C19-14", "C20-9",
            "C21-13", "C22-31", "C23-6", "C24-10", "C25-22",
            "C26-12", "C27-14", "C28-9", "C29-12", "C30-12",
            "C31-24", "C32-11", "C33-22", "C34-22", "C35-28",
            "C36-12", "C37-40", "C38-22", "C39-13", "C40-17",
            "C41-13", "C42-11", "C43-5", "C44-26", "C45-17",
            "C46-11", "C47-9", "C48-14", "C49-20", "C50-23",
            "C51-19", "C52-9", "C53-6", "C54-7", "C55-23",
            "C56-13", "C57-11", "C58-11", "C59-17", "C60-12",
            "C61-8", "C62-12", "C63-11", "C64-10", "C65-13",
            "C66-20", "C67-7", "C68-35", "C69-36", "C70-5",
            "C71-24", "C72-20", "C73-28", "C74-23", "C75-10",
            "C76-12", "C77-20", "C78-72", "C79-13", "C80-19",
            "C81-16", "C82-8", "C83-18", "C84-12", "C85-13",
            "C86-17", "C87-7", "C88-18", "C89-52", "C90-17",
            "C91-16", "C92-15", "C93-5", "C94-23", "C95-11",
            "C96-13", "C97-12", "C98-9", "C99-9", "C100-5",
            "C101-8", "C102-28", "C103-22", "C104-35", "C105-45",
            "C106-48", "C107-43", "C108-13", "C109-31", "C110-7",
            "C111-10", "C11210", "C113-9", "C114-8", "C115-18",
            "C116-19", "C117-2", "C118-29", "C119-176", "C120-7",
            "C121-8", "C122-9", "C123-4", "C124-8", "C125-5",
            "C126-6", "C127-5", "C128-6", "C129-8", "C130-8",
            "C131-3", "C132-18", "C133-3", "C1343", "C135-21",
            "C136-26", "C137-9", "C138-8", "C139-24", "C140-13",
            "C141-10", "C142-7", "C143-12", "C144-15", "C145-21", 
            "C146-10", "C147-20", "C148-14", "C149-9", "C150-6"
        };
        internal static List<string> Pro = new List<string>()
        {
            "C1-33", "C2-22", "C3-35", "C4-27", "C5-23",
            "C6-35", "C7-27", "C8-36", "C9-18", "C10-32",
            "C11-31", "C12-28", "C13-25", "C14-35", "C15-33",
            "C16-33", "C17-28", "C18-24", "C19-29", "C20-30",
            "C21-31", "C22-29", "C23-35", "C24-34", "C25-28",
            "C26-28", "C27-27", "C28-28", "C29-27", "C30-33",
            "C31-31"
        };
        internal static List<string> Ecc = new List<string>()
        {
            "C1-18", "C2-26", "C3-22", "C4-16", "C5-20",
            "C6-12", "C7-29", "C8-17", "C9-18", "C10-20",
            "C11-10", "C12-14"
        };
        internal static List<string> Sol = new List<string>()
        {
            "C1-17", "C2-17", "C3-11", "C4-16", "C5-16",
            "C6-13", "C7-13", "C8-14"
        };
        internal static List<string> Isa = new List<string>()
        {
            "C1-31", "C2-22", "C3-26", "C4-6", "C5-30",
            "C6-13", "C7-25", "C8-22", "C9-21", "C10-34",
            "C11-16", "C12-6", "C13-22", "C14-32", "C15-9",
            "C16-14", "C17-14", "C18-7", "C19-25", "C20-6",
            "C21-17", "C22-25", "C23-18", "C24-23", "C25-12",
            "C26-21", "C27-13", "C28-29", "C29-24", "C30-33",
            "C31-9", "C32-20", "C33-24", "C34-17", "C35-10",
            "C36-22", "C37-38", "C38-22", "C39-8", "C40-31",
            "C41-29", "C42-25", "C43-28", "C44-28", "C45-25",
            "C46-13", "C47-15", "C48-22", "C49-26", "C50-11",
            "C51-23", "C52-15", "C53-12", "C54-17", "C55-13",
            "C56-12", "C57-21", "C58-14", "C59-21", "C60-22",
            "C61-11", "C62-12", "C63-19", "C64-12", "C65-25",
            "C66-24"
        };
        internal static List<string> Jer = new List<string>()
        {
            "C1-19", "C2-37", "C3-25", "C4-31", "C5-31",
            "C6-30", "C7-34", "C8-22", "C9-26", "C10-25",
            "C11-23", "C12-17", "C13-27", "C14-22", "C15-21",
            "C16-21", "C17-27", "C18-23", "C19-15", "C20-18",
            "C21-14", "C22-30", "C23-40", "C24-10", "C25-38",
            "C26-24", "C27-22", "C28-17", "C29-32", "C30-24",
            "C31-40", "C32-44", "C33-26", "C34-22", "C35-19",
            "C36-32", "C37-21", "C38-28", "C39-18", "C40-16",
            "C41-18", "C42-22", "C43-13", "C44-30", "C45-5",
            "C46-28", "C47-7", "C48-47", "C49-39", "C50-46",
            "C51-64", "C52-34"
        };
        internal static List<string> Lam = new List<string>()
        {
            "C1-22", "C2-22", "C3-66", "C4-22", "C5-22"
        };
        internal static List<string> Eze = new List<string>()
        {
            "C1-28", "C2-10", "C3-27", "C4-17", "C5-17",
            "C6-14", "C7-27", "C8-18", "C9-11", "C10-22",
            "C11-25", "C12-28", "C13-23", "C14-23", "C15-8",
            "C16-63", "C17-24", "C18-32", "C19-14", "C20-49",
            "C21-32", "C22-31", "C23-49", "C24-27", "C25-17",
            "C26-21", "C27-36", "C28-26", "C29-21", "C30-26",
            "C31-18", "C32-32", "C33-33", "C34-31", "C35-15",
            "C36-38", "C37-28", "C38-23", "C39-29", "C40-49",
            "C41-26", "C42-20", "C43-27", "C44-31", "C45-25",
            "C46-24", "C47-23", "C48-35"
        };
        internal static List<string> Dan = new List<string>()
        {
            "C1-21", "C2-49", "C3-30", "C4-37", "C5-31",
            "C6-28", "C7-28", "C8-27", "C9-27", "C10-21",
            "C11-35", "C12-13"
        };
        internal static List<string> Hos = new List<string>()
        {
            "C1-11", "C2-23", "C3-5", "C4-19", "C5-15",
            "C6-11", "C7-16", "C8-14", "C9-17", "C10-15",
            "C11-12", "C12-14", "C13-16", "C14-9"
        };
        internal static List<string> Joe = new List<string>()
        {
            "C1-20", "C2-32", "C3-21"
        };
        internal static List<string> Amo = new List<string>()
        {
            "C1-15", "C2-16", "C3-15", "C4-13", "C5-27",
            "C6-14", "C7-17", "C8-14", "C9-15"
        };
        internal static List<string> Oba = new List<string>()
        {
            "C1-21"
        };
        internal static List<string> Jon = new List<string>()
        {
            "C1-16", "C2-10", "C3-10", "C4-11"
        };
        internal static List<string> Mic = new List<string>()
        {
            "C1-16", "C2-13", "C3-12", "C4-13", "C5-15",
            "C6-16", "C7-20"
        };
        internal static List<string> Nah = new List<string>()
        {
            "C1-15", "C2-13", "C3-19"
        };
        internal static List<string> Hab = new List<string>()
        {
            "C1-17", "C2-20", "C3-19"
        };
        internal static List<string> Zep = new List<string>()
        {
            "C1-18", "C2-15", "C3-20"
        };
        internal static List<string> Hag = new List<string>()
        {
            "C1-15", "C2-23"
        };
        internal static List<string> Zac = new List<string>()
        {
            "C1-21", "C2-13", "C3-10", "C4-14", "C5-11",
            "C6-15", "C7-14", "C8-23", "C9-17", "C10-12",
            "C11-17", "C12-14", "C13-9", "C14-21"
        };
        internal static List<string> Mal = new List<string>()
        {
            "C1-14", "C2-17", "C3-18", "C4-6"
        };
        internal static List<string> Mat = new List<string>()
        {
            "C1-25", "C2-23", "C3-17", "C4-25", "C5-48",
            "C6-34", "C7-29", "C8-34", "C9-38", "C10-42",
            "C11-30", "C12-50", "C13-58", "C14-36", "C15-39",
            "C16-28", "C17-27", "C18-35", "C19-30", "C20-34",
            "C21-36", "C22-46", "C23-39", "C24-51", "C25-46",
            "C26-75", "C27-66", "C28-20"
        };
        internal static List<string> Mar = new List<string>()
        {
            "C1-45", "C2-28", "C3-35", "C4-41", "C5-43",
            "C6-56", "C7-37", "C8-38", "C9-50", "C10-52",
            "C11-33", "C12-44", "C13-37", "C14-72", "C15-47",
            "C16-20"
        };
        internal static List<string> Luk = new List<string>()
        {
            "C1-80", "C2-52", "C3-38", "C4-44", "C5-39",
            "C6-49", "C7-50", "C8-56", "C9-62", "C10-42",
            "C11-54", "C12-59", "C13-35", "C14-35", "C15-32",
            "C16-31", "C17-37", "C18-43", "C19-48", "C20-47",
            "C21-38", "C22-71", "C23-56", "C24-53"
        };
        internal static List<string> Joh = new List<string>()
        {
            "C1-51", "C2-25", "C3-36", "C4-54", "C5-47",
            "C6-71", "C7-53", "C8-59", "C9-41", "C10-42",
            "C11-57", "C12-50", "C13-48", "C14-31", "C15-27",
            "C16-33", "C17-26", "C18-40", "C19-42", "C20-31",
            "C21-25"
        };
        internal static List<string> Act = new List<string>()
        {
            "C1-26", "C2-47", "C3-26", "C4-37", "C5-42",
            "C6-15", "C7-60", "C8-40", "C9-43", "C10-48",
            "C11-30", "C12-25", "C13-52", "C14-28", "C15-41",
            "C16-40", "C17-34", "C18-28", "C19-41", "C20-38",
            "C21-40", "C22-30", "C23-35", "C24-27", "C25-27",
            "C26-32", "C27-44", "C28-31"
        };
        internal static List<string> Rom = new List<string>()
        {
            "C1-32", "C2-29", "C3-31", "C4-25", "C5-21",
            "C6-23", "C7-25", "C8-39", "C9-33", "C10-21",
            "C11-36", "C12-21", "C13-14", "C14-23", "C15-33",
            "C16-27"
        };
        internal static List<string> Co1 = new List<string>()
        {
            "C1-31", "C2-16", "C3-23", "C4-21", "C5-13",
            "C6-20", "C7-40", "C8-13", "C9-27", "C10-33",
            "C11-34", "C12-31", "C13-13", "C14-40", "C15-58",
            "C16-24"
        };
        internal static List<string> Co2 = new List<string>()
        {
            "C1-24", "C2-17", "C3-18", "C4-18", "C5-21",
            "C6-18", "C7-16", "C8-24", "C9-15", "C10-18",
            "C11-33", "C12-21", "C13-14"
        };
        internal static List<string> Gal = new List<string>()
        {
            "C1-24", "C2-21", "C3-29", "C4-31", "C5-26",
            "C6-18"
        };
        internal static List<string> Eph = new List<string>()
        {
            "C1-23", "C2-22", "C3-21", "C4-32", "C5-33",
            "C6-24"
        };
        internal static List<string> Phi = new List<string>()
        {
            "C1-30", "C2-30", "C3-21", "C4-23"
        };
        internal static List<string> Col = new List<string>()
        {
            "C1-29", "C2-23", "C3-25", "C4-18"
        };
        internal static List<string> Th1 = new List<string>()
        {
            "C1-10", "C2-20", "C3-13", "C4-18", "C5-28"
        };
        internal static List<string> Th2 = new List<string>()
        {
            "C1-12", "C2-17", "C3-18"
        };
        internal static List<string> Ti1 = new List<string>()
        {
            "C1-20", "C2-15", "C3-16", "C4-16", "C5-25",
            "C6-21"
        };
        internal static List<string> Ti2 = new List<string>()
        {
            "C1-18", "C2-26", "C3-17", "C4-22"
        };
        internal static List<string> Tit = new List<string>()
        {
            "C1-16", "C2-15", "C3-15"
        };
        internal static List<string> Phm = new List<string>()
        {
            "C1-25"
        };
        internal static List<string> Heb = new List<string>()
        {
            "C1-14", "C2-18", "C3-19", "C4-16", "C5-14",
            "C6-20", "C7-28", "C8-13", "C9-28", "C10-39",
            "C11-40", "C12-29", "C13-25"
        };
        internal static List<string> Jam = new List<string>()
        {
            "C1-27", "C2-26", "C3-18", "C4-17", "C5-20"
        };
        internal static List<string> Pe1 = new List<string>()
        {
            "C1-25", "C2-25", "C3-22", "C4-19", "C5-14"
        };
        internal static List<string> Pe2 = new List<string>()
        {
            "C1-21", "C2-22", "C3-18"
        };
        internal static List<string> Jo1 = new List<string>()
        {
            "C1-10", "C2-29", "C3-24", "C4-21", "C5-21"
        };
        internal static List<string> Jo2 = new List<string>()
        {
            "C1-13"
        };
        internal static List<string> Jo3 = new List<string>()
        {
            "C1-14"
        };
        internal static List<string> Jde = new List<string>()
        {
            "C1-25"
        };
        internal static List<string> Rev = new List<string>()
        {
            "C1-20", "C2-29", "C3-22", "C4-11", "C5-14",
            "C6-17", "C7-17", "C8-13", "C9-21", "C10-11",
            "C11-19", "C12-17", "C13-18", "C14-20", "C15-8",
            "C16-21", "C17-18", "C18-24", "C19-21", "C20-15",
            "C21-27", "C22-21"
        };
        #endregion
        internal static int GetVerseLimit(Book book, int chapter)
        {
            int i = (int)book;
            Bible.Abrevations c = (Bible.Abrevations)i;
            string abrev = c.ToString();
            if (abrev == "Gen")
            {
                if (chapter < 51 && chapter > 0)
                    return Convert.ToInt32(Gen[chapter - 1].Split('-')[1]);
                else
                    return 0;             
            }
            else if (abrev == "Exo")
            {
                if (chapter < 41 && chapter > 0)
                    return Convert.ToInt32(Exo[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Lev")
            {
                if (chapter < 28 && chapter > 0)
                    return Convert.ToInt32(Lev[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Num")
            {
                if (chapter < 37 && chapter > 0)
                    return Convert.ToInt32(Num[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Deu")
            {
                if (chapter < 35 && chapter > 0)
                    return Convert.ToInt32(Deu[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Jos")
            {
                if (chapter < 25 && chapter > 0)
                    return Convert.ToInt32(Jos[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Jdg")
            {
                if (chapter < 22 && chapter > 0)
                    return Convert.ToInt32(Jdg[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Rut")
            {
                if (chapter < 5 && chapter > 0)
                    return Convert.ToInt32(Rut[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Sa1")
            {
                if (chapter < 32 && chapter > 0)
                    return Convert.ToInt32(Sa1[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Sa2")
            {
                if (chapter < 25 && chapter > 0)
                    return Convert.ToInt32(Sa2[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Kg1")
            {
                if (chapter < 23 && chapter > 0)
                    return Convert.ToInt32(Kg1[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Kg2")
            {
                if (chapter < 26 && chapter > 0)
                    return Convert.ToInt32(Kg2[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Ch1")
            {
                if (chapter < 30 && chapter > 0)
                    return Convert.ToInt32(Ch1[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Ch2")
            {
                if (chapter < 37 && chapter > 0)
                    return Convert.ToInt32(Ch2[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Ezr")
            {
                if (chapter < 11 && chapter > 0)
                    return Convert.ToInt32(Ezr[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Neh")
            {
                if (chapter < 14 && chapter > 0)
                    return Convert.ToInt32(Neh[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Est")
            {
                if (chapter < 11 && chapter > 0)
                    return Convert.ToInt32(Est[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Job")
            {
                if (chapter < 43 && chapter > 0)
                    return Convert.ToInt32(Job[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Psa")
            {
                if (chapter < 151 && chapter > 0)
                    return Convert.ToInt32(Psa[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Pro")
            {
                if (chapter < 32 && chapter > 0)
                    return Convert.ToInt32(Pro[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Ecc")
            {
                if (chapter < 13 && chapter > 0)
                    return Convert.ToInt32(Ecc[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Sol")
            {
                if (chapter < 9 && chapter > 0)
                    return Convert.ToInt32(Sol[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Isa")
            {
                if (chapter < 67 && chapter > 0)
                    return Convert.ToInt32(Isa[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Jer")
            {
                if (chapter < 53 && chapter > 0)
                    return Convert.ToInt32(Jer[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Lam")
            {
                if (chapter < 6 && chapter > 0)
                    return Convert.ToInt32(Lam[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Eze")
            {
                if (chapter < 49 && chapter > 0)
                    return Convert.ToInt32(Eze[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Dan")
            {
                if (chapter < 13 && chapter > 0)
                    return Convert.ToInt32(Dan[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Hos")
            {
                if (chapter < 15 && chapter > 0)
                    return Convert.ToInt32(Hos[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Joe")
            {
                if (chapter < 4 && chapter > 0)
                    return Convert.ToInt32(Joe[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Amo")
            {
                if (chapter < 10 && chapter > 0)
                    return Convert.ToInt32(Amo[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Oba")
            {
                if (chapter < 2 && chapter > 0)
                    return Convert.ToInt32(Oba[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Jon")
            {
                if (chapter < 5 && chapter > 0)
                    return Convert.ToInt32(Jon[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Mic")
            {
                if (chapter < 8 && chapter > 0)
                    return Convert.ToInt32(Mic[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Nah")
            {
                if (chapter < 4 && chapter > 0)
                    return Convert.ToInt32(Nah[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Hab")
            {
                if (chapter < 4 && chapter > 0)
                return Convert.ToInt32(Hab[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Zep")
            {
                if (chapter < 4 && chapter > 0)
                    return Convert.ToInt32(Zep[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Hag")
            {
                if (chapter < 3 && chapter > 0)
                    return Convert.ToInt32(Hag[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Zac")
            {
                if (chapter < 15 && chapter > 0)
                    return Convert.ToInt32(Zac[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Mal")
            {
                if (chapter < 5 && chapter > 0)
                    return Convert.ToInt32(Mal[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Mat")
            {
                if (chapter < 29 && chapter > 0)
                    return Convert.ToInt32(Mat[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Mar")
            {
                if (chapter < 17 && chapter > 0)
                    return Convert.ToInt32(Mar[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Luk")
            {
                if (chapter < 25 && chapter > 0)
                    return Convert.ToInt32(Luk[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Joh")
            {
                if (chapter < 22 && chapter > 0)
                    return Convert.ToInt32(Joh[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Act")
            {
                if (chapter < 29 && chapter > 0)
                    return Convert.ToInt32(Act[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Rom")
            {
                if (chapter < 17 && chapter > 0)
                    return Convert.ToInt32(Rom[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Co1")
            {
                if (chapter < 17 && chapter > 0)
                    return Convert.ToInt32(Co1[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Co2")
            {
                if (chapter < 14 && chapter > 0)
                    return Convert.ToInt32(Co2[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Gal")
            {
                if (chapter < 7 && chapter > 0)
                    return Convert.ToInt32(Gal[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Eph")
            {
                if (chapter < 7 && chapter > 0)
                    return Convert.ToInt32(Eph[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Phi")
            {
                if (chapter < 5 && chapter > 0)
                    return Convert.ToInt32(Phi[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Col")
            {
                if (chapter < 5 && chapter > 0)
                    return Convert.ToInt32(Col[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Th1")
            {
                if (chapter < 6 && chapter > 0)
                    return Convert.ToInt32(Th1[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Th2")
            {
                if (chapter < 4 && chapter > 0)
                    return Convert.ToInt32(Th2[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Ti1")
            {
                if (chapter < 7 && chapter > 0)
                    return Convert.ToInt32(Ti1[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Ti2")
            {
                if (chapter < 5 && chapter > 0)
                    return Convert.ToInt32(Ti2[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Tit")
            {
                if (chapter < 4 && chapter > 0)
                    return Convert.ToInt32(Tit[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Phm")
            {
                if (chapter < 2 && chapter > 0)
                    return Convert.ToInt32(Phm[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Heb")
            {
                if (chapter < 14 && chapter > 0)
                    return Convert.ToInt32(Heb[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Jam")
            {
                if (chapter < 6 && chapter > 0)
                    return Convert.ToInt32(Jam[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Pe1")
            {
                if (chapter < 6 && chapter > 0)
                    return Convert.ToInt32(Pe1[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Pe2")
            {
                if (chapter < 4 && chapter > 0)
                    return Convert.ToInt32(Pe2[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Jo1")
            {
                if (chapter < 6 && chapter > 0)
                    return Convert.ToInt32(Jo1[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Jo2")
            {
                if (chapter < 2 && chapter > 0)
                    return Convert.ToInt32(Jo2[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Jo3")
            {
                if (chapter < 2 && chapter > 0)
                    return Convert.ToInt32(Jo3[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Jde")
            {
                if (chapter < 2 && chapter > 0)
                    return Convert.ToInt32(Jde[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else if (abrev == "Rev")
            {
                if (chapter < 23 && chapter > 0)
                    return Convert.ToInt32(Rev[chapter - 1].Split('-')[1]);
                else
                    return 0;
            }
            else
            {
                return 0;
            }
        }
        internal static string TranslateBook(string abrevation)
        {
            int indextoGet = BookKeys.IndexOf(abrevation);
            Book b = (Book)indextoGet;
            return b.ToString();
        }
        internal static string TranslateAbrevation(Book book)
        {
            //Index of selected book:
            int i = (int)book;
            Bible.Abrevations c = (Bible.Abrevations)i;
            //Abrevation of selected book:
            return c.ToString();
        }
        internal static Dictionary<string, string> FilterKeys = new Dictionary<string, string>()
        {
            {"New_Testament", "Mat-Mar-Luk-Joh-Act-Rom-Co1-Co2-Gal-Eph-Phi-Col-Th1-Th2-Ti1-Ti2-Tit-Phm-Heb-Jam-Pe1-Pe2-Jo1-Jo2-Jo3-Jde-Rev"},
            {"Old_Testament", "Gen-Exo-Lev-Num-Deu-Jos-Jdg-Rut-Sa1-Sa2-Kg1-Kg2-Ch1-Ch2-Ezr-Neh-Est-Job-Psa-Pro-Ecc-Sol-Isa-Jer-Lam-Eze-Dan-Hos-Joe-Amo-Oba-Jon-Mic-Nah-Hab-Zep-Hag-Zac-Mal"},
            {"Pentateuch", "Gen-Exo-Lev-Num-Deu"},
            {"Historical_Books", "Jos-Jdg-Rut-Sa1-Sa2-Kg1-Kg2-Ch1-Ch2-Ezr-Neh-Est"},
            {"Poetical_and_Wisdom_Books", "Job-Psa-Pro-Ecc-Sol-Lam"},
            {"The_Prophets", "Isa-Jer-Eze-Dan-Hos-Joe-Amo-Oba-Jon-Mic-Nah-Hab-Zep-Hag-Zac-Mal"},
            {"The_Gospels", "Mat-Mar-Luk-Joh"},
            {"The_Acts", "Act"},
            {"Pauls_Letters", "Rom-Co1-Co2-Gal-Eph-Phi-Col-Th1-Th2-Ti1-Ti2-Tit-Phm"},
            {"General_Epistles_and_Revelation", "Heb-Jam-Pe1-Pe2-Jo1-Jo2-Jo3-Jde-Rev"}
        };
        internal static List<string> BookKeys = new List<string>()
        {
            {"Gen"},
            {"Exo"},
            {"Lev"},
            {"Num"},
            {"Deu"},
            {"Jos"},
            {"Jdg"},
            {"Rut"},
            {"Sa1"},
            {"Sa2"},
            {"Kg1"},
            {"Kg2"},
            {"Ch1"},
            {"Ch2"},
            {"Ezr"},
            {"Neh"},
            {"Est"},
            {"Job"},
            {"Psa"},
            {"Pro"},
            {"Ecc"},
            {"Sol"},
            {"Isa"},
            {"Jer"},
            {"Lam"},
            {"Eze"},
            {"Dan"},
            {"Hos"},
            {"Joe"},
            {"Amo"},
            {"Oba"},
            {"Jon"},
            {"Mic"},
            {"Nah"},
            {"Hab"},
            {"Zep"},
            {"Hag"},
            {"Zac"},
            {"Mal"},
            {"Mat"},
            {"Mar"},
            {"Luk"},
            {"Joh"},
            {"Act"},
            {"Rom"},
            {"Co1"},
            {"Co2"},
            {"Gal"},
            {"Eph"},
            {"Phi"},
            {"Col"},
            {"Th1"},
            {"Th2"},
            {"Ti1"},
            {"Ti2"},
            {"Tit"},
            {"Phm"},
            {"Heb"},
            {"Jam"},
            {"Pe1"},
            {"Pe2"},
            {"Jo1"},
            {"Jo2"},
            {"Jo3"},
            {"Jde"},
            {"Rev"}
        };   
    }

    /// <summary>
    /// Exception for the event when a specified chapter is out of range.
    /// </summary>
    public class ChapterOutOfRangeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of a ChapterOutOfRangeException.
        /// </summary>
        /// <param name="message"></param>
        public ChapterOutOfRangeException(string message) : base(message) { }
    }

    /// <summary>
    /// Exception for the event when a specified verse is out of range.
    /// </summary>
    public class VerseOutOfRangeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of a VerseOutOfRangeException.
        /// </summary>
        /// <param name="message"></param>
        public VerseOutOfRangeException(string message) : base(message) { }
    }
}
