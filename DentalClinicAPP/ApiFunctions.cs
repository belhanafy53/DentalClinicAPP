

using Net.Pkcs11Interop.Common;
using Net.Pkcs11Interop.HighLevelAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Ess;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace DentalClinicAPP
{


    public static class Variables
    {

        //public static readonly string TokenLink = "https://id.preprod.eta.gov.eg/connect/token";
        //public static readonly string SubmitDocument = "https://api.preprod.invoicing.eta.gov.eg/api/v1/documentsubmissions";
        //public static readonly string ContentType = "application/x-www-form-urlencoded";
        public static readonly string DllLibPath = "eps2003csp11.dll";
        public static string TokenPin;
        public static Hasher _hasher;
        public static Serializer _serializer;
        public static IConfiguration _configuration;
    }


    #region Clases
    public class Token
    {
        /// <summary>
        /// Encoded JWT token structure that contains the fields of the issued token and also token protection attributes
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// The lifetime of the access token defined in seconds
        /// </summary>
        public int expires_in { get; set; }
        /// <summary>
        /// Solution in this case returns only Bearer authentication tokens
        /// </summary>
        public string token_type { get; set; }
        /// <summary>
        /// Optional if matches the requested scope. Otherwise contains information on scope granted to token. This defines the APIs that client will have access to using this token.
        /// </summary>
        public string scope { get; set; }



    }
    public partial class Signature
    {
      
        public string signatureType { get; set; }
        public string value1 { get; set; } // فقط تعريف واحد للخاصية 'value'
    }
    public class Outputs
    {
        public string submissionUUID { get; set; }
        public AcceptedDocuments[] acceptedDocuments { get; set; }
        public DocumentRejected[] rejectedDocuments { get; set; }
    }

    public class OutputLogin
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
    }
    public class AcceptedDocuments
    {
        public string uuid { get; set; }

        public string longId { get; set; }

        public string internalId { get; set; }
    }
    public class Error
    {
        public string code { get; set; }
        public string target { get; set; }

        public string message { get; set; }

        public Error[] details { get; set; }
    }
    public class DocumentRejected
    {
        public string internalId { get; set; }

        public Error error { get; set; }
    }
    #endregion

    public class ApiFunctions
    {
        public string Serialize(JObject request)
        {
            return this.SerializeToken(request);
        }


        public static JObject SignatureJson(string tokenPin, InvoiceHeader InvoiceHeaderObj)
        {
            ApiFunctions singer = new ApiFunctions();
            Variables.TokenPin = tokenPin;
            // string content = "";
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                FloatFormatHandling = FloatFormatHandling.String,
                FloatParseHandling = FloatParseHandling.Decimal,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateParseHandling = DateParseHandling.None
            };

            //JObject request =JsonConvert.DeserializeObject<JObject>(JasonText);
            CreatJason CreatJasonObj = new CreatJason();
            CreatJason.DocumentObj DocumentObj = new CreatJason.DocumentObj();
            string jsonobj = CreatJasonObj.createJsonObj(InvoiceHeaderObj);
            JObject gettj = singer.GetTokenSigner(jsonobj, tokenPin);
            return gettj;
            // DocumentObj = CreatJasonObj.CreatObj(InvoiceHeaderObj);

            // JObject request = JObject.FromObject(DocumentObj);
            // ETASerialization.Serializer SerlizeObj = new ETASerialization.Serializer();
            // string serializedJson = SerlizeObj.Serialize(request);//singer.Serialize(request);
            // content = singer.SignWithCMS(serializedJson,Variables.DllLibPath,Variables.TokenPin);
            // EFProcess.InvoiceHeaderDB InvoiceDb = new EFProcess.InvoiceHeaderDB();
            //InvoiceHeaderObj.documentTypeVersion = "I";
            // InvoiceHeaderObj.signaturevalue = content;
            // bool updates = InvoiceDb.UpdateRecord(InvoiceHeaderObj);
            // InvoiceHeaderObj.documentTypeVersion = "1.0";
            // string json = CreatJasonObj.createJsonSig(InvoiceHeaderObj);

            // //object[] objArray1 = new object[] { new JProperty("signatureType", "I"), new JProperty("value", content) };
            // //JObject obj3 = new JObject(objArray1);
            // //JArray array = new JArray { obj3 };
            // //request.Add("signatures", array);
            // return "{\"documents\":[" + json + "]}";
            //return content;

        }

        private byte[] Hash(string input)
        {
            using (SHA256 sha = SHA256.Create())
            {
                return sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            }
        }

        private string SerializeToken(JToken request)
        {
            string str = "";
            if (request.Parent == null)
            {
                this.SerializeToken(request.First);
            }
            else if (request.Type == JTokenType.Property)
            {
                string str2 = ((JProperty)request).Name.ToUpper();
                str = str + "\"" + str2 + "\"";
                foreach (JToken token in request)
                {
                    if (token.Type == JTokenType.Object)
                    {
                        str = str + this.SerializeToken(token);
                    }
                    if (((token.Type == JTokenType.Boolean) || (token.Type == JTokenType.Integer)) || ((token.Type == JTokenType.Float) || (token.Type == JTokenType.Date)))
                    {
                        str = str + "\"" + Extensions.Value<string>((IEnumerable<JToken>)token) + "\"";
                    }
                    if (token.Type == JTokenType.String)
                    {
                        str = str + JsonConvert.ToString(Extensions.Value<string>((IEnumerable<JToken>)token));
                    }
                    if (token.Type == JTokenType.Array)
                    {
                        foreach (JToken token2 in token.Children())
                        {
                            str = str + "\"" + ((JProperty)request).Name.ToUpper() + "\"";
                            str = str + this.SerializeToken(token2);
                        }
                    }
                }
            }
            if (request.Type == JTokenType.Object)
            {
                foreach (JToken token3 in request.Children())
                {
                    if ((token3.Type == JTokenType.Object) || (token3.Type == JTokenType.Property))
                    {
                        str = str + this.SerializeToken(token3);
                    }
                }
            }
            return str;
        }

        private byte[] HashBytes(byte[] input)
        {
            using (SHA256 sha = SHA256.Create())
            {
                return sha.ComputeHash(input);
            }
        }

        private void ListCertificates()
        {
            //using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
                X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.MaxAllowed);
            X509Certificate2Enumerator enumerator = store.Certificates.Find(X509FindType.FindByThumbprint, "b7cd5e56012c69a7613f49204133ade91fa54daf", false).GetEnumerator();
           
            while (enumerator.MoveNext())
            {
                X509Certificate2 current = enumerator.Current;
                try
                {
                    byte[] rawData = current.RawData;
                    Console.WriteLine("Content Type: {0}{1}", X509Certificate2.GetCertContentType(rawData), Environment.NewLine);
                    Console.WriteLine("Friendly Name: {0}{1}", current.FriendlyName, Environment.NewLine);
                    Console.WriteLine("Certificate Verified?: {0}{1}", (bool)current.Verify(), Environment.NewLine);
                    Console.WriteLine("Simple Name: {0}{1}", current.GetNameInfo(X509NameType.SimpleName, true), Environment.NewLine);
                    Console.WriteLine("Signature Algorithm: {0}{1}", current.SignatureAlgorithm.FriendlyName, Environment.NewLine);
                    Console.WriteLine("Public Key: {0}{1}", current.PublicKey.Key.ToXmlString(false), Environment.NewLine);
                    Console.WriteLine("Certificate Archived?: {0}{1}", (bool)current.Archived, Environment.NewLine);
                    Console.WriteLine("Length of Raw Data: {0}{1}", (int)current.RawData.Length, Environment.NewLine);
                    current.Reset();
                    continue;
                }
                catch (CryptographicException exception1)
                {
                    Console.WriteLine("Information could not be written out for this certificate.");
                    throw exception1;
                }
            }
            store.Close();
        }

        public JObject GetTokenSigner(string docArr, string tokenPin)
        {

            Encoding utf8 = Encoding.UTF8;
            Serializer serializer = new Serializer();

            string s = "";
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.FloatFormatHandling = FloatFormatHandling.String;
            settings.FloatParseHandling = FloatParseHandling.Decimal;
            settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            settings.DateParseHandling = DateParseHandling.None;
            JObject request = JsonConvert.DeserializeObject<JObject>(docArr, settings);
            string str2 = serializer.Serialize(request);
            s = (Extensions.Value<string>((IEnumerable<JToken>)request["documentTypeVersion"]) != "0.9") ? SignWithCMS(str2, Variables.DllLibPath, tokenPin) : "ANY";
            object[] content = new object[] { new JProperty("signatureType", "I"), new JProperty("value", s) };
            JArray array = new JArray {
                    new JObject(content)
                };
            request.Add("signatures", array);
            return request;
            //  string str3 = "{\"documents\":[" + request.ToString() + "]}";
            // return JsonConvert.DeserializeObject<JObject>(request.ToString());
            // return str3;
        }

        public string SignWithCMS(string serializedJson, string DllLibPath, string TokenPin)
        {
            byte[] data = Encoding.UTF8.GetBytes(serializedJson);
            Pkcs11InteropFactories factories = new Pkcs11InteropFactories();
            using (IPkcs11Library pkcs11Library = factories.Pkcs11LibraryFactory.LoadPkcs11Library(factories, DllLibPath, AppType.MultiThreaded))
            {
                ISlot slot = pkcs11Library.GetSlotList(SlotsType.WithTokenPresent).FirstOrDefault();

                if (slot == null)
                {
                    return "No slots found";
                }

                ITokenInfo tokenInfo = slot.GetTokenInfo();

                ISlotInfo slotInfo = slot.GetSlotInfo();


                using (var session = slot.OpenSession(SessionType.ReadWrite))
                {

                    session.Login(CKU.CKU_USER  , Encoding.UTF8.GetBytes(TokenPin));

                    var certificateSearchAttributes = new List<IObjectAttribute>()
                    {
                        session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE),
                        session.Factories.ObjectAttributeFactory.Create(CKA.CKA_TOKEN, true),
                        session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CERTIFICATE_TYPE, CKC.CKC_X_509)
                    };

                    IObjectHandle certificate = session.FindAllObjects(certificateSearchAttributes).FirstOrDefault();

                    if (certificate == null)
                    {
                        return "Certificate not found";
                    }

                    X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                    store.Open(OpenFlags.MaxAllowed);

                    // find cert by thumbprint
                    //var foundCerts = store.Certificates.Find(X509FindType.FindByIssuerName, "s Egypt Trust CA G6 ID", true);

                    var foundCerts = store.Certificates.Find(X509FindType.FindByThumbprint , "b7cd5e56012c69a7613f49204133ade91fa54daf", false);



                    if (foundCerts.Count == 0)
                        return "no device detected";

                    var certForSigning = foundCerts[0];
                    store.Close();


                    ContentInfo content = new ContentInfo(new Oid("1.2.840.113549.1.7.5"), data);


                    SignedCms cms = new SignedCms(content, true);

                    EssCertIDv2 bouncyCertificate = new EssCertIDv2(new Org.BouncyCastle.Asn1.X509.AlgorithmIdentifier(new DerObjectIdentifier("1.2.840.113549.1.9.16.2.47")), HashBytes(certForSigning.RawData));

                    SigningCertificateV2 signerCertificateV2 = new SigningCertificateV2(new EssCertIDv2[] { bouncyCertificate });


                    CmsSigner signer = new CmsSigner(certForSigning);

                    signer.DigestAlgorithm = new Oid("2.16.840.1.101.3.4.2.1");



                    signer.SignedAttributes.Add(new Pkcs9SigningTime(DateTime.UtcNow));
                    signer.SignedAttributes.Add(new AsnEncodedData(new Oid("1.2.840.113549.1.9.16.2.47"), signerCertificateV2.GetEncoded()));


                    cms.ComputeSignature(signer);

                    var output = cms.Encode();


                    return Convert.ToBase64String(output);
                }
            }

        }




        //private string SignWithCMS(string serializedJson)
        //{
        //    string str;
        //    byte[] bytes = Encoding.UTF8.GetBytes(serializedJson);
        //    Pkcs11InteropFactories factories = new Pkcs11InteropFactories();
        //    using (IPkcs11Library library = factories.Pkcs11LibraryFactory.LoadPkcs11Library(factories, Variables.DllLibPath, AppType.MultiThreaded))
        //    {
        //        ISlot slot = Enumerable.FirstOrDefault<ISlot>((IEnumerable<ISlot>)library.GetSlotList(SlotsType.WithTokenPresent));
        //        if (slot == null)
        //        {
        //            str = "No slots found";
        //        }
        //        else
        //        {
        //            slot.GetTokenInfo();
        //            slot.GetSlotInfo();
        //            using (ISession session = slot.OpenSession(SessionType.ReadWrite))
        //            {

        //                session.Login(CKU.CKU_USER, Encoding.UTF8.GetBytes(Variables.TokenPin));
        //                List<IObjectAttribute> list = new List<IObjectAttribute> {
        //                session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE),
        //                session.Factories.ObjectAttributeFactory.Create(CKA.CKA_TOKEN, true),
        //                session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CERTIFICATE_TYPE, CKC.CKC_X_509)
        //        };
        //                if (Enumerable.FirstOrDefault<IObjectHandle>((IEnumerable<IObjectHandle>)session.FindAllObjects(list)) == null)
        //                {
        //                    return "Certificate not found";
        //                }
        //                X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        //                store.Open(OpenFlags.MaxAllowed);
        //                X509Certificate2Collection certificates = store.Certificates.Find(X509FindType.FindByIssuerName, "Egypt Trust Sealing CA", false);
        //                if (certificates.Count == 0)
        //                {
        //                    return "no device detected";
        //                }
        //                X509Certificate2 certificate = certificates[0];
        //                store.Close();
        //                SignedCms cms = new SignedCms(new ContentInfo(new Oid("1.2.840.113549.1.7.5"), bytes), true);
        //                EssCertIDv2 dv = new EssCertIDv2(new Org.BouncyCastle.Asn1.X509.AlgorithmIdentifier(new DerObjectIdentifier("1.2.840.113549.1.9.16.2.47")), this.HashBytes(certificate.RawData));
        //                EssCertIDv2[] certs = new EssCertIDv2[] { dv };
        //                SigningCertificateV2 ev = new SigningCertificateV2(certs);
        //                CmsSigner signer = new CmsSigner(certificate)
        //                {
        //                    DigestAlgorithm = new Oid("2.16.840.1.101.3.4.2.1")
        //                };
        //                signer.SignedAttributes.Add(new Pkcs9SigningTime(DateTime.UtcNow));
        //                signer.SignedAttributes.Add(new AsnEncodedData(new Oid("1.2.840.113549.1.9.16.2.47"), ev.GetEncoded()));
        //                cms.ComputeSignature(signer);
        //                str = Convert.ToBase64String(cms.Encode());
        //            }
        //        }
        //    }
        //    return str;
        //}

        ////private string SignWithCMS(string serializedJson)
        //{
        //    string str;
        //    byte[] data = Encoding.UTF8.GetBytes(serializedJson);
        //    Pkcs11InteropFactories factories = new Pkcs11InteropFactories();
        //    using (IPkcs11Library pkcs11Library = factories.Pkcs11LibraryFactory.LoadPkcs11Library(factories, Variables.DllLibPath, AppType.MultiThreaded))
        //    {
        //        ISlot slot = pkcs11Library.GetSlotList(SlotsType.WithTokenPresent).FirstOrDefault();

        //        if (slot == null)
        //        {
        //            return "No slots found";
        //        }


        //        using (var session = slot.OpenSession(SessionType.ReadWrite))
        //        {

        //            session.Login(CKU.CKU_USER, Encoding.UTF8.GetBytes(Variables.TokenPin));

        //            var searchAttribute = new List<IObjectAttribute>()
        //            {
        //                session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_CERTIFICATE),
        //                session.Factories.ObjectAttributeFactory.Create(CKA.CKA_TOKEN, true),
        //                session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CERTIFICATE_TYPE, CKC.CKC_X_509)
        //            };

        //            IObjectHandle certificate = session.FindAllObjects(searchAttribute).FirstOrDefault();


        //            if (certificate == null)
        //            {
        //                return "Certificate not found";
        //            }

        //            var attributeValues = session.GetAttributeValue(certificate, new List<CKA>
        //            {
        //                CKA.CKA_VALUE
        //            });


        //            var xcert = new X509Certificate2(attributeValues[0].GetValueAsByteArray());

        //            searchAttribute = new List<IObjectAttribute>()
        //            {
        //                session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_PRIVATE_KEY),
        //                session.Factories.ObjectAttributeFactory.Create(CKA.CKA_KEY_TYPE,CKK.CKK_RSA)
        //            };

        //            IObjectHandle privateKeyHandler = session.FindAllObjects(searchAttribute).FirstOrDefault();

        //            RSA privateKey = new TokenRSA(xcert, session, slot, privateKeyHandler);
        //            //privateKey.ImportRSAPublicKey(_cspBlob, out _);

        //            //searchAttribute = new List<IObjectAttribute>()
        //            //{
        //            //    session.Factories.ObjectAttributeFactory.Create(CKA.CKA_CLASS, CKO.CKO_PRIVATE_KEY),
        //            //    session.Factories.ObjectAttributeFactory.Create(CKA.CKA_KEY_TYPE,CKK.CKK_RSA)
        //            //};

        //            //IObjectHandle privateKeyHandler = session.FindAllObjects(searchAttribute).FirstOrDefault();

        //            //attributeValues = session.GetAttributeValue(privateKeyHandler, new List<CKA> { 
        //            //    CKA.CKA_VALUE 
        //            //});

        //            //RSA privateKey = RSA.Create();
        //            //privateKey.ImportRSAPrivateKey(attributeValues[0].GetValueAsByteArray(), out _);





        //            ContentInfo content = new ContentInfo(new Oid("1.2.840.113549.1.7.5"), data);


        //            SignedCms cms = new SignedCms(content, true);

        //            Hasher Haserobj = new Hasher();

        //            EssCertIDv2 bouncyCertificate = new EssCertIDv2(new Org.BouncyCastle.Asn1.X509.AlgorithmIdentifier(new DerObjectIdentifier("1.2.840.113549.1.9.16.2.47")), Haserobj.HashBytes(xcert.RawData));

        //            SigningCertificateV2 signerCertificateV2 = new SigningCertificateV2(new EssCertIDv2[] { bouncyCertificate });


        //            CmsSigner signer = new CmsSigner(xcert);


        //            // signer.PrivateKey = privateKey;

        //            signer.DigestAlgorithm = new Oid("2.16.840.1.101.3.4.2.1");



        //            signer.SignedAttributes.Add(new Pkcs9SigningTime(DateTime.UtcNow));
        //            signer.SignedAttributes.Add(new AsnEncodedData(new Oid("1.2.840.113549.1.9.16.2.47"), signerCertificateV2.GetEncoded()));

        //            cms.ComputeSignature(signer);

        //            var output = cms.Encode();

        //            return Convert.ToBase64String(output);
        //        }
        //    }

        //}
        //public class cmssignr : CmsSigner
        //{
        //    public AsymmetricAlgorithm? PrivateKey { get; set; }
        //}

        public class APIParamter
        {
            public string ParamterName { get; set; }
            public string ParamterValue { get; set; }
        }

        private static void CreateJasonFile(string JsonText)
        {
            string TempLocation = Path.GetTempPath() + "DynamicLinks Json Test\\" + Convert.ToString(DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss")) + ".txt";
            string txt = JsonText;
            File.WriteAllText(TempLocation, txt);
        }

        public static string SendAndReceiveRequest(string API_Link, Method method, string Token, string API_HeaderName, string API_HeaderValue, List<APIParamter> parameters)
        {
            var client = new RestClient(API_Link);
            var request = new RestRequest(); // انشئ الطلب بدون تحديد الطريقة هنا

            // تعيين طريقة الطلب
            request.Method = method;

            if (!string.IsNullOrWhiteSpace(Token))
            {
                request.AddHeader("Authorization", $"Bearer {Token}");
            }

            if (!string.IsNullOrWhiteSpace(API_HeaderName) && !string.IsNullOrWhiteSpace(API_HeaderValue))
            {
                request.AddHeader(API_HeaderName, API_HeaderValue);
            }

            foreach (var param in parameters)
            {
                request.AddParameter(param.ParamterName, param.ParamterValue);
            }

            var response = client.Execute(request);

            return response.Content;
        }

        /* ----------------------   This function to post data to API ----------------------------------- */
       public static string GetToken(string URL, string clientID, string clientSecret)
{
    var options = new RestClientOptions(URL)
    {
        Timeout = TimeSpan.FromMilliseconds(-1)  // تعيين وقت الانتظار إلى قيمة غير محدودة
    };
    var client = new RestClient(options);

    var request = new RestRequest
    {
        Method = Method.Post  // تعيين نوع الطلب هنا
    };

    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
    request.AddParameter("grant_type", "client_credentials");
    request.AddParameter("client_id", clientID);
    request.AddParameter("client_secret", clientSecret);

    var response = client.Execute(request);

    if (response.IsSuccessful)
    {
        var mydata = response.Content;
        dynamic deserialized = JsonConvert.DeserializeObject(mydata);
        var acctoken = deserialized.access_token;
        var tokentype = deserialized.token_type;
        return $"Bearer {acctoken}";
    }
    else
    {
        throw new Exception($"Error: {response.StatusCode} - {response.Content}");
    }
}
        public static CustomResponse PostData(string submitLink, string TokenKey, string JsonInvData)
        {
            var options = new RestClientOptions(submitLink)
            {
                Timeout = TimeSpan.FromMinutes(-1)  // تعيين وقت الانتظار هنا
            };
            var client = new RestClient(options);

            var request = new RestRequest
            {
                Method = Method.Post  // تعيين نوع الطلب هنا
            };

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", TokenKey);
            request.AddJsonBody(JsonInvData);  // استخدام AddJsonBody بدلاً من AddParameter

            var response = client.Execute(request);

            // تحويل الاستجابة إلى JSON
            dynamic json = JsonConvert.DeserializeObject(response.Content);
            CustomResponse customResponse = new CustomResponse();

            // التحقق من الاستجابة              
            if (json["submissionId"] != null)
            {
                customResponse.submissionID = json["submissionId"];
                customResponse.uuid = json["acceptedDocuments"][0]["uuid"];
                customResponse.longId = json["acceptedDocuments"][0]["longId"];
                customResponse.hashKey = json["acceptedDocuments"][0]["hashKey"];
            }
            else
            {
                customResponse.errorCode = json["rejectedDocuments"][0]["error"]["code"];
                customResponse.erroeMessage = json["rejectedDocuments"][0]["error"]["message"];
                customResponse.errorTarget = json["rejectedDocuments"][0]["error"]["target"];
            }

            return customResponse;
        }
        /* ----------------------   This function to get document status ----------------------------------- */
        static void GetDocStatus(string TokenKey, string clientID, string clientSecret, string uuid)
        {
            var url = $"https://id.eta.gov.eg/api/v1.0/documents/{uuid}/raw";

            var options = new RestClientOptions(url)
            {
                Timeout = TimeSpan.FromMinutes(-1) // تعيين وقت الانتظار
            };

            var client = new RestClient(options);
            var request = new RestRequest
            {
                Method = Method.Get // تعيين نوع الطلب هنا
            };

            // إضافة رؤوس الطلب
            request.AddHeader("Authorization", TokenKey);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            // تنفيذ الطلب
            var response = client.Execute(request);

            // عرض محتوى الاستجابة
            Console.WriteLine(response.Content);
        }
    }


}
