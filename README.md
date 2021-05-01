# CSBible
C# package for KJV Bible book, chapter, and verse retrieval.



## Class CSBible.Bible

CSBible.Bible is a class for Bible actions.  To use it, first create an instance:

`using CSBible;`

`Bible b = new Bible();`

Now you can use the verse functions.  For example, using GetVerse():

`string verse = b.GetVerse(Book.Genesis, 1, 1);`

The method GetChapter() returns a string array of verses that make up the specified chapter:

`string[] chapter = b.GetChapter(Book.Genesis, 1);`

NOTE: The array that this method returns is not zero-based (i.e. index 1 is verse 1, index 2 is verse 2, etc.).

When you are finished, call Close():

`b.Close();`

#### For information on the methods introduced in version 2.0.0, see the XML documentation in Visual Studio

#### The current methods and their functions are listed below:
* `GetVerse(Book book, int chapter, int verse)`
* `GetChapter(Book book, int chapter)`
* `GetChapter(Book book, int chapter, bool zeroBased)`
* `GetChapterAsList(Book book, int chapter)`
* `GetChapterAsList(Book book, int chapter, bool zeroBased)`
##### Added in version 2.0.0:
* `Find(string query)`
* `Find(string query, Filter filter)`
* `Find(string query, Book book)`
* `Search(string query)`
* `Search(string query, Filter filter)`
* `Search(string query, Book book)`

#### For questions or comments, post them in the "Issues" tab above.
