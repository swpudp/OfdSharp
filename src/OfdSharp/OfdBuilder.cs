using OfdSharp.Primitives;
using OfdSharp.Primitives.Entry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace OfdSharp
{
    public class OfdBuilder
    {
        private static readonly Dictionary<DocUsage, string> DocUsages = new Dictionary<DocUsage, string>
        {
            [DocUsage.Normal] = "Normal",
            [DocUsage.EBook] = "EBook",
            [DocUsage.ENewsPaper] = "EMagazine",
            [DocUsage.EMagazine] = "EMagazine"
        };

        private readonly OfdRoot _ofdRoot = new OfdRoot();
        private int _docIndex = -1;
        private readonly List<OfdDocument> _documents = new List<OfdDocument>();

        private static string GetDocUsageDesc(DocUsage? docUsage)
        {
            return docUsage.HasValue ? DocUsages[docUsage.Value] : null;
        }

        public OfdDocument AddDocument(OfdDocumentInfo documentInfo)
        {
            Interlocked.Increment(ref _docIndex);
            var docBody = new DocBody
            {
                DocInfo = new CtDocInfo
                {
                    DocId = Guid.NewGuid().ToString("N"),
                    Title = documentInfo.Title,
                    Author = documentInfo.Author,
                    Subject = documentInfo.Subject,
                    Abstract = documentInfo.Abstract,
                    CreationDate = DateTime.Now,
                    DocUsage = GetDocUsageDesc(documentInfo.DocUsage),
                    Cover = documentInfo.Cover,
                    Keywords = documentInfo.Keywords,
                    Creator = documentInfo.Creator,
                    CreatorVersion = documentInfo.CreatorVersion,
                    CustomDataList = documentInfo.CustomDataList
                },
                DocRoot = new CtLocation($"Doc_{_docIndex}/Document.xml")
            };
            _ofdRoot.DocBodyList.Add(docBody);
            OfdDocument ofdDocument = new OfdDocument(_docIndex);
            _documents.Add(ofdDocument);
            return ofdDocument;
        }

        public byte[] Save()
        {
            return _documents.Select(document => document.Save(_ofdRoot)).FirstOrDefault();
        }
    }
}