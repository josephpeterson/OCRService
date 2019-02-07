using System;
using Tesseract;

namespace OCRService
{
    public interface IOCRService
    {
        string ConvertImage(string stuff);
    }
    public class OCRService : IOCRService
    {
        private TesseractEngine _engine;
        public OCRService()
        {
            //Initialize Tesseract Engine
            _engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
        }

        public string ConvertImage(string imagePath)
        {
            var output = String.Empty;

            try
            {
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using (var page = _engine.Process(img))
                    {
                        var text = page.GetText();
                        Console.WriteLine("Mean confidence: {0}", page.GetMeanConfidence());

                        output = text;
                        Console.WriteLine("Text (GetText): \r\n{0}", text);
                        Console.WriteLine("Text (iterator):");
                        using (var iter = page.GetIterator())
                        {
                            iter.Begin();

                            do
                            {
                                do
                                {
                                    do
                                    {
                                        do
                                        {
                                            if (iter.IsAtBeginningOf(PageIteratorLevel.Block))
                                            {
                                                Console.WriteLine("<BLOCK>");
                                            }

                                            Console.Write(iter.GetText(PageIteratorLevel.Word));
                                            Console.Write(" ");

                                            if (iter.IsAtFinalOf(PageIteratorLevel.TextLine, PageIteratorLevel.Word))
                                            {
                                                Console.WriteLine();
                                            }
                                        } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));

                                        if (iter.IsAtFinalOf(PageIteratorLevel.Para, PageIteratorLevel.TextLine))
                                        {
                                            Console.WriteLine();
                                        }
                                    } while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                                } while (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                            } while (iter.Next(PageIteratorLevel.Block));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected Error: " + e.Message);
                Console.WriteLine("Details: ");
                Console.WriteLine(e.ToString());
            }
            return output;
       }
    }
}
