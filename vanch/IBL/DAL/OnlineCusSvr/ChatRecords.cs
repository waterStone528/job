using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;

namespace DAL.OnlineCusSvr
{
    public class ChatRecords
    {
        public void SaveChatRecords(DataTable dt)
        {
            using (VanchBgDataContext vdc = new VanchBgDataContext())
            {
                var linq = (from c in dt.AsEnumerable()
                            select new chat_record
                            {
                                user_num = c.Field<string>("userNum"),
                                cus_svr_num = c.Field<string>("cusSvrNum"),
                                user_send_msg = c.Field<string>("userSendMsg"),
                                cus_svr_send_msg = c.Field<string>("cusSvrSendMsg"),
                                date_time = c.Field<DateTime>("dateTime")
                            }).ToList();

                vdc.chat_records.InsertAllOnSubmit(linq);
                vdc.SubmitChanges();
            }
        }
    }
}
