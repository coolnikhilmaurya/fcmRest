using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;


namespace FCMHelper
{
    public class FCM
    {
       
        public static string title = "title";
        public static string body = "body";
        public static string click_action = "click_action";
        public static string icon = "icon";

        // url for generating urls for server iid urls
        private string url;

        // for storing fcm credincials
        private string FCM_SERVER_KEY { get; set; }
        private string FCM_SENDER_ID { get; set; }

        // constructor for getting FCM_SERVER_KEY and FCM_SERVER_KEY 
        public FCM(string FCM_SERVER_KEY, string FCM_SENDER_ID)
        {
            // getting the fcm server credintials via constructor only
            this.FCM_SERVER_KEY = FCM_SERVER_KEY;
            this.FCM_SENDER_ID = FCM_SENDER_ID;
        }


        public string Send_To_Topic(string body, string title, string topic)
        {
            var payload = new
            {
                to = "/topics/" + topic,
                priority = "high",
                content_available = true,
                data = new
                {
                    body,
                    title,
                },
            };

            return sendFCMMessage(payload);
        } 


        public string Send_Data_Message_To_Topic(Dictionary<string,string> dic, string topic)
        {
            var payload = new
            {
                to = "/topics/" + topic,
                priority = "high",
                content_available = true,
                data = dic,
            };

            return sendFCMMessage(payload);
        }


        public string Send_Data_Message_To_FcmToken(Dictionary<string, string> dataValues, string fcmToken)
        {
            var payload = new
            {
                to = fcmToken,
                priority = "high",
                content_available = true,
                data = dataValues,
            };

            return sendFCMMessage(payload);
        }


        public string Send_Notification_To_Topic(string title, string body, string topic)
        {
            var payload = new
            {
                to = "/topics/" + topic,
                priority = "high",
                content_available = true,
                notification = new
                {
                    body,
                    title,
                    badge = 1
                },
            };

            return sendFCMMessage(payload);
        }


        public string Send_Web_Notification_To_FcmToken(string title, string body, string click_action, string icon, string token)
        {
            var payload = new
            {
                to = token,
                priority = "high",
                content_available = true,
                notification = new
                {
                    body,
                    title,
                    click_action,
                    icon,
                },
            };

            return sendFCMMessage(payload);
        }


        public string Send_Web_Notification_To_Topic(string title, string body, string click_action, string icon, string topic)
        {
            var payload = new
            {
                to = "/topics/" + topic,
                priority = "high",
                content_available = true,
                notification = new
                {
                    body,
                    title,
                    click_action,
                    icon,
                },
            };

            return sendFCMMessage(payload);
        }
        

        public string SubscribeToTopicViaToken(string token, string topic)
        {
            url = string.Format("https://iid.googleapis.com/iid/v1/{0}/rel/topics/{1}", token, topic);

            return postIIDRequest(url);
        }


        // max 1000 tokens allowed
        public string SubscribeToTopicViaBatchTokens(List<string> tokens, string topic)
        {
            url = "https://iid.googleapis.com/iid/v1:batchAdd";
            var payload = new
            {
                to = "/topics/" + topic,
                registration_tokens = tokens,
            };

            return postIIDRequest(url, payload);
        }

        public string UnSubscribeToTopicViaBatchTokens(List<string> tokens, string topic)
        {
            url = "https://iid.googleapis.com/iid/v1:batchRemove";
            var payload = new
            { 
                to = "/topics/" + topic,
                registration_tokens = tokens,
            };

            return postIIDRequest(url, payload);
        }


        public string getInfoAboutFcmToken(string token)
        {
            //<summary>
            //this is the summary for getInfoAboutFcmToken
            //</summary>
            url = string.Format("https://iid.googleapis.com/iid/info/{0}?details=true", token);

            return getIIDRequest(url);
        }


        private string sendFCMMessage(object payload)
        {
            string result = string.Empty;
            try
            {
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                //serverKey - Key from Firebase cloud messaging server  
                tRequest.Headers.Add(string.Format("Authorization: key={0}", FCM_SERVER_KEY));
                //Sender Id - From firebase project setting  
                tRequest.Headers.Add(string.Format("Sender: id={0}", FCM_SENDER_ID));
                tRequest.ContentType = "application/json";

                string postbody = JsonConvert.SerializeObject(payload).ToString();
                byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    result = tReader.ReadToEnd();
                                    if (result.Contains("message"))
                                    {
                                        result = "Message send successfully";
                                    }
                                }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result = "Couldn't send message due to server error. Make sure you've replsced FCM_SERVER_KEY and FCM_SENDER_ID";
            }
            return result;
        }


        private string postIIDRequest(string url, object payload = null)
        {
            string result = string.Empty;
            try
            {
                WebRequest tRequest = WebRequest.Create(url);
                tRequest.Method = "post";
                //serverKey - Key from Firebase cloud messaging server  
                tRequest.Headers.Add(string.Format("Authorization: key={0}", FCM_SERVER_KEY));
                tRequest.ContentType = "application/json";

                string postbody = JsonConvert.SerializeObject(payload).ToString();
                byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
                tRequest.ContentLength = byteArray.Length;

                //Sender Id - From firebase project setting  
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            HttpStatusCode statusCode = ((HttpWebResponse)tResponse).StatusCode;
                            if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    result = tReader.ReadToEnd();
                                    if (statusCode == HttpStatusCode.OK)
                                    {
                                        result+= "Operation Success";
                                    }
                                }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result = "Couldn't send message due to server error.";
            }
            return result;
        }


        private string getIIDRequest(string url)
        {
            string result = string.Empty;
            try
            {
                WebRequest tRequest = WebRequest.Create(url);
                //serverKey - Key from Firebase cloud messaging server  
                tRequest.Headers.Add(string.Format("Authorization: key={0}", FCM_SERVER_KEY));
                //Sender Id - From firebase project setting 
                using (HttpWebResponse response = (HttpWebResponse)tRequest.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }

            }
            catch (Exception ex)
            {
                result = "Couldn't send message due to server error.";
            }
            return result;
        }

    }

}
