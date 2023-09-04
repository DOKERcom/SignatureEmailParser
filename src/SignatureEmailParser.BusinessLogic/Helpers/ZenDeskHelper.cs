using System;
using SignatureEmailParser.BusinessLogic.Constants;
using System.Diagnostics;

namespace SignatureEmailParser.BusinessLogic.Helpers
{
    public class ZenDeskHelper : IZenDeskHelper
    {
        string _login;
        string _password;

        public ZenDeskHelper()
        {
            _login = ZendeskProperties.LOGIN;
            _password = ZendeskProperties.PASSWORD;
        }

        public bool CreateNewTicket(string ticketSub, string ticketBody, string tokenFile, string priority)
        {
            Chilkat.Global global = new Chilkat.Global();

            bool successLock = global.UnlockBundle("SCLHTZ.CB1042022_azvMCzy9D51l");
            if (successLock != true)
            {
                string error = global.LastErrorText;
                return false;
            }

            if (ShouldCreateNewTicket(ticketSub))
            {
                Chilkat.Rest rest = new Chilkat.Rest();

                //  URL: https://subdomain.zendesk.com/api/v2/tickets.json
                bool bTls = true;
                int port = 443;
                bool bAutoReconnect = true;
                bool taskSuccess = rest.Connect("https://yourdomain.zendesk.com", port, bTls, bAutoReconnect);
                if (taskSuccess != true)
                {
                    //Debug.WriteLine("ConnectFailReason: " + Convert.ToString(rest.ConnectFailReason));
                    //Debug.WriteLine(rest.LastErrorText);
                    return false;
                }

                rest.SetAuthBasic(_login, _password);

                Chilkat.JsonObject json = new Chilkat.JsonObject();
                
                //      json.UpdateString("ticket.comment.body", "Computer Name: " + Program.ComputerName + "\r\n" + "OSVersion: " + Program.OSVersion + "\r\n\r\n" + ticketBody);
                json.UpdateString("ticket.subject", ticketSub);
                json.UpdateString("ticket.comment.body", ticketBody);
                json.UpdateString("ticket.priority", priority);
                json.UpdateString("ticket.type", "problem");
                json.UpdateString("ticket.group_id", ZendeskProperties.GROUPID);
                json.UpdateString("ticket.requester_id", ZendeskProperties.REQUESTERID);
                json.UpdateString("ticket.comment.uploads[0]", tokenFile);

                rest.AddHeader("Content-Type", "application/json");
                rest.AddHeader("Accept", "application/json");

                Chilkat.StringBuilder sbRequestBody = new Chilkat.StringBuilder();
                json.EmitSb(sbRequestBody);
                Chilkat.StringBuilder sbResponseBody = new Chilkat.StringBuilder();


                bool success = rest.FullRequestSb("POST", "/api/v2/tickets", sbRequestBody, sbResponseBody);
                if (success != true)
                {
                    Debug.WriteLine(rest.LastErrorText);
                    return false;
                }

                int respStatusCode = rest.ResponseStatusCode;
                if (respStatusCode >= 400)
                {
                    Debug.WriteLine("Response Status Code = " + Convert.ToString(respStatusCode));
                    Debug.WriteLine("Response Header:");
                    Debug.WriteLine(rest.ResponseHeader);
                    Debug.WriteLine("Response Body:");
                    Debug.WriteLine(sbResponseBody.GetAsString());
                    return false;
                }

                Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
                jsonResponse.LoadSb(sbResponseBody);

                string ticketUrl;
                int ticketId;
                string ticketExternal_id;
                string ticketViaChannel;
                string ticketViaSourceRel;
                string ticketCreated_at;
                string ticketUpdated_at;
                string ticketType;
                string ticketSubject;
                string ticketRaw_subject;
                string ticketDescription;
                string ticketPriority;
                string ticketStatus;
                string ticketRecipient;
                int ticketRequester_id;
                int ticketSubmitter_id;
                int ticketAssignee_id;
                int ticketOrganization_id;
                int ticketGroup_id;
                string ticketForum_topic_id;
                string ticketProblem_id;
                bool ticketHas_incidents;
                bool ticketIs_public;
                string ticketDue_at;
                string ticketSatisfaction_rating;
                int ticketBrand_id;
                bool ticketAllow_channelback;
                int auditId;
                int auditTicket_id;
                string auditCreated_at;
                int auditAuthor_id;
                string auditMetadataSystemIp_address;
                string auditMetadataSystemLocation;
                int auditMetadataSystemLatitude;
                int auditMetadataSystemLongitude;
                string auditViaChannel;
                string auditViaSourceRel;
                int i;
                int count_i;
                int id;
                string type;
                int author_id;
                string body;
                string html_body;
                string plain_body;
                bool publics;
                int audit_id;
                string value;
                string field_name;
                string viaChannel;
                bool viaSourceFromDeleted;
                string viaSourceFromTitle;
                int viaSourceFromId;
                string viaSourceRel;
                string subject;
                int j;
                int count_j;
                int intVal;

                ticketUrl = jsonResponse.StringOf("ticket.url");
                ticketId = jsonResponse.IntOf("ticket.id");
                ticketExternal_id = jsonResponse.StringOf("ticket.external_id");
                ticketViaChannel = jsonResponse.StringOf("ticket.via.channel");
                ticketViaSourceRel = jsonResponse.StringOf("ticket.via.source.rel");
                ticketCreated_at = jsonResponse.StringOf("ticket.created_at");
                ticketUpdated_at = jsonResponse.StringOf("ticket.updated_at");
                ticketType = jsonResponse.StringOf("ticket.type");
                ticketSubject = jsonResponse.StringOf("ticket.subject");
                ticketRaw_subject = jsonResponse.StringOf("ticket.raw_subject");
                ticketDescription = jsonResponse.StringOf("ticket.description");
                ticketPriority = jsonResponse.StringOf("ticket.priority");
                ticketStatus = jsonResponse.StringOf("ticket.status");
                ticketRecipient = jsonResponse.StringOf("ticket.recipient");
                ticketRequester_id = jsonResponse.IntOf("ticket.requester_id");
                ticketSubmitter_id = jsonResponse.IntOf("ticket.submitter_id");
                ticketAssignee_id = jsonResponse.IntOf("ticket.assignee_id");
                ticketOrganization_id = jsonResponse.IntOf("ticket.organization_id");
                ticketGroup_id = jsonResponse.IntOf("ticket.group_id");
                ticketForum_topic_id = jsonResponse.StringOf("ticket.forum_topic_id");
                ticketProblem_id = jsonResponse.StringOf("ticket.problem_id");
                ticketHas_incidents = jsonResponse.BoolOf("ticket.has_incidents");
                ticketIs_public = jsonResponse.BoolOf("ticket.is_public");
                ticketDue_at = jsonResponse.StringOf("ticket.due_at");
                ticketSatisfaction_rating = jsonResponse.StringOf("ticket.satisfaction_rating");
                ticketBrand_id = jsonResponse.IntOf("ticket.brand_id");
                ticketAllow_channelback = jsonResponse.BoolOf("ticket.allow_channelback");
                auditId = jsonResponse.IntOf("audit.id");
                auditTicket_id = jsonResponse.IntOf("audit.ticket_id");
                auditCreated_at = jsonResponse.StringOf("audit.created_at");
                auditAuthor_id = jsonResponse.IntOf("audit.author_id");
                auditMetadataSystemIp_address = jsonResponse.StringOf("audit.metadata.system.ip_address");
                auditMetadataSystemLocation = jsonResponse.StringOf("audit.metadata.system.location");
                auditMetadataSystemLatitude = jsonResponse.IntOf("audit.metadata.system.latitude");
                auditMetadataSystemLongitude = jsonResponse.IntOf("audit.metadata.system.longitude");
                auditViaChannel = jsonResponse.StringOf("audit.via.channel");
                auditViaSourceRel = jsonResponse.StringOf("audit.via.source.rel");
                i = 0;
                count_i = jsonResponse.SizeOfArray("ticket.collaborator_ids");
                while (i < count_i)
                {
                    jsonResponse.I = i;
                    i = i + 1;
                }

                i = 0;
                count_i = jsonResponse.SizeOfArray("ticket.follower_ids");
                while (i < count_i)
                {
                    jsonResponse.I = i;
                    i = i + 1;
                }

                i = 0;
                count_i = jsonResponse.SizeOfArray("ticket.email_cc_ids");
                while (i < count_i)
                {
                    jsonResponse.I = i;
                    i = i + 1;
                }

                i = 0;
                count_i = jsonResponse.SizeOfArray("ticket.tags");
                while (i < count_i)
                {
                    jsonResponse.I = i;
                    i = i + 1;
                }

                i = 0;
                count_i = jsonResponse.SizeOfArray("ticket.custom_fields");
                while (i < count_i)
                {
                    jsonResponse.I = i;
                    i = i + 1;
                }

                i = 0;
                count_i = jsonResponse.SizeOfArray("ticket.sharing_agreement_ids");
                while (i < count_i)
                {
                    jsonResponse.I = i;
                    i = i + 1;
                }

                i = 0;
                count_i = jsonResponse.SizeOfArray("ticket.fields");
                while (i < count_i)
                {
                    jsonResponse.I = i;
                    i = i + 1;
                }

                i = 0;
                count_i = jsonResponse.SizeOfArray("ticket.followup_ids");
                while (i < count_i)
                {
                    jsonResponse.I = i;
                    i = i + 1;
                }

                i = 0;
                count_i = jsonResponse.SizeOfArray("audit.events");
                while (i < count_i)
                {
                    jsonResponse.I = i;
                    id = jsonResponse.IntOf("audit.events[i].id");
                    type = jsonResponse.StringOf("audit.events[i].type");
                    author_id = jsonResponse.IntOf("audit.events[i].author_id");
                    body = jsonResponse.StringOf("audit.events[i].body");
                    html_body = jsonResponse.StringOf("audit.events[i].html_body");
                    plain_body = jsonResponse.StringOf("audit.events[i].plain_body");
                    publics = jsonResponse.BoolOf("audit.events[i].public");
                    audit_id = jsonResponse.IntOf("audit.events[i].audit_id");
                    value = jsonResponse.StringOf("audit.events[i].value");
                    field_name = jsonResponse.StringOf("audit.events[i].field_name");
                    viaChannel = jsonResponse.StringOf("audit.events[i].via.channel");
                    viaSourceFromDeleted = jsonResponse.BoolOf("audit.events[i].via.source.from.deleted");
                    viaSourceFromTitle = jsonResponse.StringOf("audit.events[i].via.source.from.title");
                    viaSourceFromId = jsonResponse.IntOf("audit.events[i].via.source.from.id");
                    viaSourceRel = jsonResponse.StringOf("audit.events[i].via.source.rel");
                    subject = jsonResponse.StringOf("audit.events[i].subject");
                    j = 0;
                    count_j = jsonResponse.SizeOfArray("audit.events[i].attachments");
                    while (j < count_j)
                    {
                        jsonResponse.J = j;
                        j = j + 1;
                    }

                    j = 0;
                    count_j = jsonResponse.SizeOfArray("audit.events[i].recipients");
                    while (j < count_j)
                    {
                        jsonResponse.J = j;
                        intVal = jsonResponse.IntOf("audit.events[i].recipients[j]");
                        j = j + 1;
                    }

                    i = i + 1;
                }

                return true;
            }

            return false;
        }

        public bool ShouldCreateNewTicket(string ticketSubject)
        {
            Chilkat.Global global = new Chilkat.Global();

            bool successLock = global.UnlockBundle("SCLHTZ.CB1042022_azvMCzy9D51l");
            if (successLock != true)
            {
                string error = global.LastErrorText;
                return false;
            }

            Chilkat.Rest rest = new Chilkat.Rest();
            bool success;

            //  URL: https://subdomain.zendesk.com/api/v2/search.json
            bool bTls = true;
            int port = 443;
            bool bAutoReconnect = true;
            success = rest.Connect("https://yourdomain.zendesk.com", port, bTls, bAutoReconnect);
            if (success != true)
            {
                //Debug.WriteLine("ConnectFailReason: " + Convert.ToString(rest.ConnectFailReason));
                //Debug.WriteLine(rest.LastErrorText);
                return false;
            }

            rest.SetAuthBasic(_login, _password);

            rest.AddQueryParam("query", "type:ticket status:open subject:" + ticketSubject);

            rest.AddHeader("Accept", "application/json");

            string strResponseBody = rest.FullRequestFormUrlEncoded("GET", "/api/v2/search.json");
            if (rest.LastMethodSuccess != true)
            {
                //Debug.WriteLine(rest.LastErrorText);
                return false;
            }

            int respStatusCode = rest.ResponseStatusCode;
            if (respStatusCode >= 400)
            {
                Debug.WriteLine("Response Status Code = " + Convert.ToString(respStatusCode));
                Debug.WriteLine("Response Header:");
                Debug.WriteLine(rest.ResponseHeader);
                Debug.WriteLine("Response Body:");
                Debug.WriteLine(strResponseBody);
                return false;
            }

            Chilkat.JsonObject jsonResponse = new Chilkat.JsonObject();
            jsonResponse.Load(strResponseBody);

            int result = jsonResponse.SizeOfArray("results");

            if (result == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

            string facets;
            string next_page;
            string previous_page;
            int count;
            int i;
            int count_i;
            string url;
            int id;
            string external_id;
            string viaChannel;
            string viaSourceRel;
            string created_at;
            string updated_at;
            string type;
            string subject;
            string raw_subject;
            string description;
            string priority;
            string status;
            string recipient;
            int requester_id;
            int submitter_id;
            int assignee_id;
            int organization_id;
            int group_id;
            string forum_topic_id;
            string problem_id;
            bool has_incidents;
            bool is_public;
            string due_at;
            string satisfaction_rating;
            int brand_id;
            bool allow_channelback;
            string result_type;
            int j;
            int count_j;
            string strVal;

            facets = jsonResponse.StringOf("facets");
            next_page = jsonResponse.StringOf("next_page");
            previous_page = jsonResponse.StringOf("previous_page");
            count = jsonResponse.IntOf("count");
            i = 0;
            count_i = jsonResponse.SizeOfArray("results");
            while (i < count_i)
            {
                jsonResponse.I = i;
                url = jsonResponse.StringOf("results[i].url");
                id = jsonResponse.IntOf("results[i].id");
                external_id = jsonResponse.StringOf("results[i].external_id");
                viaChannel = jsonResponse.StringOf("results[i].via.channel");
                viaSourceRel = jsonResponse.StringOf("results[i].via.source.rel");
                created_at = jsonResponse.StringOf("results[i].created_at");
                updated_at = jsonResponse.StringOf("results[i].updated_at");
                type = jsonResponse.StringOf("results[i].type");
                subject = jsonResponse.StringOf("results[i].subject");
                raw_subject = jsonResponse.StringOf("results[i].raw_subject");
                description = jsonResponse.StringOf("results[i].description");
                priority = jsonResponse.StringOf("results[i].priority");
                status = jsonResponse.StringOf("results[i].status");
                recipient = jsonResponse.StringOf("results[i].recipient");
                requester_id = jsonResponse.IntOf("results[i].requester_id");
                submitter_id = jsonResponse.IntOf("results[i].submitter_id");
                assignee_id = jsonResponse.IntOf("results[i].assignee_id");
                organization_id = jsonResponse.IntOf("results[i].organization_id");
                group_id = jsonResponse.IntOf("results[i].group_id");
                forum_topic_id = jsonResponse.StringOf("results[i].forum_topic_id");
                problem_id = jsonResponse.StringOf("results[i].problem_id");
                has_incidents = jsonResponse.BoolOf("results[i].has_incidents");
                is_public = jsonResponse.BoolOf("results[i].is_public");
                due_at = jsonResponse.StringOf("results[i].due_at");
                satisfaction_rating = jsonResponse.StringOf("results[i].satisfaction_rating");
                brand_id = jsonResponse.IntOf("results[i].brand_id");
                allow_channelback = jsonResponse.BoolOf("results[i].allow_channelback");
                result_type = jsonResponse.StringOf("results[i].result_type");
                j = 0;
                count_j = jsonResponse.SizeOfArray("results[i].collaborator_ids");
                while (j < count_j)
                {
                    jsonResponse.J = j;
                    j = j + 1;
                }

                j = 0;
                count_j = jsonResponse.SizeOfArray("results[i].follower_ids");
                while (j < count_j)
                {
                    jsonResponse.J = j;
                    j = j + 1;
                }

                j = 0;
                count_j = jsonResponse.SizeOfArray("results[i].email_cc_ids");
                while (j < count_j)
                {
                    jsonResponse.J = j;
                    j = j + 1;
                }

                j = 0;
                count_j = jsonResponse.SizeOfArray("results[i].tags");
                while (j < count_j)
                {
                    jsonResponse.J = j;
                    strVal = jsonResponse.StringOf("results[i].tags[j]");
                    j = j + 1;
                }

                j = 0;
                count_j = jsonResponse.SizeOfArray("results[i].custom_fields");
                while (j < count_j)
                {
                    jsonResponse.J = j;
                    j = j + 1;
                }

                j = 0;
                count_j = jsonResponse.SizeOfArray("results[i].sharing_agreement_ids");
                while (j < count_j)
                {
                    jsonResponse.J = j;
                    j = j + 1;
                }

                j = 0;
                count_j = jsonResponse.SizeOfArray("results[i].fields");
                while (j < count_j)
                {
                    jsonResponse.J = j;
                    j = j + 1;
                }

                j = 0;
                count_j = jsonResponse.SizeOfArray("results[i].followup_ids");
                while (j < count_j)
                {
                    jsonResponse.J = j;
                    j = j + 1;
                }

                i = i + 1;
            }
        }
    }
}
