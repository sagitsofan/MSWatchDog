using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using MSWatchDog.Modules.DashManifest;
using MSWatchDog.Helper;

namespace MSWatchDog.Logic
{
    public class EndPoint
    {
        public EndPoint(string _Url, string _Name)
        {
            this.Url = _Url;
            this.Name = _Name;
        }

        /// <summary>
        /// Proccess over all methods
        /// </summary>
        /// <returns></returns>
        public bool Process()
        {
            bool isVaild = false;

            try
            {

                //retrive manifest
                var Manifest = GetManifest();

                if (Manifest != null)
                {
                    string segmentUrl = string.Empty;
                    bool validStructure = checkStructure(Manifest, out segmentUrl);

                    if (validStructure)
                    {
                        //check the segment response headers
                        isVaild = checkSegment(Url + "/" + segmentUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError("Process", ex);
                throw ex;
            }
            finally
            {
                Logger.Write(string.Format("Finish job Id: {0}, Valid: {1}, Url: {2}", this.Name, isVaild, this.Url));
            }

            return isVaild;
        }

        /// <summary>
        /// Check if the manifest structure is valid
        /// </summary>
        /// <param name="Manifest"></param>
        /// <param name="SegmentUrl"></param>
        /// <returns></returns>
        private bool checkStructure(MPDtype Manifest, out string SegmentUrl)
        {
            try
            {
                //check manifest data structure
                if (Manifest != null &&
                    Manifest.Period != null &&
                    Manifest.Period.Length > 0)
                {
                    if (Manifest.Period[0] != null &&
                        Manifest.Period[0].AdaptationSet != null &&
                        Manifest.Period[0].AdaptationSet.Length > 0)
                    {
                        if (Manifest.Period[0].AdaptationSet[0] != null &&
                            Manifest.Period[0].AdaptationSet[0].SegmentTemplate != null)
                        {
                            if (Manifest.Period[0].AdaptationSet[0].Representation != null &&
                                Manifest.Period[0].AdaptationSet[0].Representation.Length > 0)
                            {
                                if (Manifest.Period[0].AdaptationSet[0].Representation[0] != null)
                                {
                                    //get properties from manifest for the segment url
                                    var _segmentUrl = Manifest.Period[0].AdaptationSet[0].SegmentTemplate.media;
                                    var _bandwidth = Manifest.Period[0].AdaptationSet[0].Representation[0].bandwidth.ToString();

                                    //some replacing jobs for the segment url
                                    _segmentUrl = _segmentUrl.Replace("$Bandwidth$", _bandwidth);
                                    _segmentUrl = _segmentUrl.Replace("$Time$", "0");

                                    SegmentUrl = _segmentUrl;
                                    return true;
                                }
                            }
                        }
                    }
                }

                SegmentUrl = string.Empty;
                return false;
            }
            catch (Exception ex)
            {
                Logger.WriteError("checkStructure", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Get manifest xml data from url
        /// </summary>
        /// <returns></returns>
        private MPDtype GetManifest()
        {
            try
            {
                WebRequest request = WebRequest.Create(this.ManifestUrl);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    Console.WriteLine(response.StatusDescription);
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(dataStream))
                        {
                            return reader.ReadToEnd().Deserialize<MPDtype>();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError("GetManifest", ex);
                throw ex;
            }
        }

        /// <summary>
        /// Check if segment response header is currect
        /// </summary>
        /// <param name="segmentUrl"></param>
        /// <returns></returns>
        private bool checkSegment(string segmentUrl)
        {
            try
            {
                WebRequest request = WebRequest.Create(segmentUrl);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return response.Headers["content-type"] == Consts.VIDEO_MP4_FORMAT;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError("checkSegment", ex);
                throw ex;
            }
        }

        private string Name { get; set; }
        private string Url { get; set; }

        private string ManifestUrl
        {
            get
            {
                return Url + Consts.MANIFEST_EXTENTION;
            }
        }
    }
}
