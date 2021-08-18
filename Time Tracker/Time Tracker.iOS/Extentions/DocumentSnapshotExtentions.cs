using Firebase.CloudFirestore;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace Time_Tracker.iOS.Extentions
{
    public static class DocumentSnapshotExtentions
    {
        public static T Convert<T>(this DocumentSnapshot doc)
        {
            var dict = new Dictionary<string, object>();
            var docDict = doc.Data;
            foreach (var key in docDict.Keys)
            {
                var val = docDict[key];
                if (val is NSString str)
                {
                    dict[key.ToString()] = str.ToString();
                }
                else if (val is NSNumber num)
                {
                    var numStr = num.ToString();
                    if (numStr.Contains("."))
                    {
                        dict[key.ToString()] = num.DoubleValue;
                    }
                    else
                    {
                        dict[key.ToString()] = num.Int32Value;
                    }
                }                
            }
            var tJson = Newtonsoft.Json.JsonConvert.SerializeObject(dict);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(tJson);
        }
    }
}