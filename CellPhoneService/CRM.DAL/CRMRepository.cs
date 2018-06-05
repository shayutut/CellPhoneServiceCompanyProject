using CRM.CommonFiles.Interfaces;
using CRM.CommonFiles.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace CRM.DAL
{
    public class CRMRepository : ICRMRepository
    {
        public IEnumerable<ClientType> GetClientTypes()
        {
            using (PhoneCompanyContext context = new PhoneCompanyContext())
            {
                var t = context.ClientTypes.ToList();
                return t;
            }
        }
        public void AddClient(Client client)
        {
            if (client != null)
                using (PhoneCompanyContext context = new PhoneCompanyContext())
                {
                    context.Entry(client.TypeId).State = System.Data.Entity.EntityState.Unchanged;
                    context.Entry(client).State = System.Data.Entity.EntityState.Added;
                    context.Clients.Add(client);
                    context.SaveChanges();
                }
        }

        public Client GetClientById(string Id)
        {
            using (PhoneCompanyContext context = new PhoneCompanyContext())
            {
                return context.Clients.FirstOrDefault(c => c.Id == Id);
            }
        }

        public IEnumerable<Line> GetLinesByClientId(string Id)
        {
            using (PhoneCompanyContext context = new PhoneCompanyContext())
            {
                //var x=context.Lines.Include(li=>li.Package).Where(l=>l.ClientId==Id).ToList();
                //var x = context.Clients.Include(c => c.Lines).Where(c => c.Id == Id).ToList();
                var t = context.Lines.Where(l => l.ClientId == Id).ToList();
                return t;
            }
        }

        public void SendCall(Call call)
        {
            if (call != null)
                using (PhoneCompanyContext context = new PhoneCompanyContext())
                {
                    try
                    {
                    //context.Entry(call.Line).State = System.Data.Entity.EntityState.Unchanged;
                    //context.Entry(call.Line.Package).State = System.Data.Entity.EntityState.Unchanged;
                    context.Entry(call).State = System.Data.Entity.EntityState.Added;
                    context.Calls.Add(call);
                    context.SaveChanges();

                    }
                    catch (Exception x)
                    {
                        Debug.Write(x.Message);
                        throw;
                    }
                }
        }

        public void SendCall(SMS sMS)
        {
            if (sMS != null)
                using (PhoneCompanyContext context = new PhoneCompanyContext())
                {
                    context.Entry(sMS.Line).State = System.Data.Entity.EntityState.Unchanged;
                    //context.Entry(sMS.Line.Package).State = System.Data.Entity.EntityState.Unchanged;
                    context.Entry(sMS).State = System.Data.Entity.EntityState.Added;
                    context.SMSs.Add(sMS);
                    context.SaveChanges();
                }
        }

        public IEnumerable<Client> GetClients()
        {
            using (PhoneCompanyContext context = new PhoneCompanyContext())
            {
                var t = context.Clients;
                if (t != null)
                    return t.ToList();
                else return null;
            }
        }

        public IEnumerable<Line> GetLines()
        {
            using (PhoneCompanyContext context = new PhoneCompanyContext())
            {
                var t = context.Lines.ToList();
                return t;
            }
        }

        public IEnumerable<Package> GetPackages()
        {
            using (PhoneCompanyContext context = new PhoneCompanyContext())
            {
                var t = context.Packages.ToList();
                return t;
            }
        }

        public void AddLine(Line line)
        {
            if (line != null)
                using (PhoneCompanyContext context = new PhoneCompanyContext())
                {
                    try
                    {
                        //if(line.Client != null)
                        //{
                        //    line.ClientId = line.Client.Id;
                        //    line.Client = null;
                        //}
                        //if (line.Package != null)
                        //{
                        //    line.PackageId = line.Package.Id;
                        //    line.Package = null;
                        //}
                        context.Lines.Add(line);
                        //context.Entry(line.Client).State = System.Data.Entity.EntityState.Unchanged;
                        //context.Entry(line.Package).State = System.Data.Entity.EntityState.Unchanged;
                        context.Entry(line).State = System.Data.Entity.EntityState.Added;
                        context.SaveChanges();
                    }
                    catch (Exception x)
                    {
                        Debug.Write(x.Message);
                    }
                }
        }

        public void AddPackageDetails(PackageInclude packageInclude)
        {
            if (packageInclude != null)
                using (PhoneCompanyContext context = new PhoneCompanyContext())
                {
                    try
                    {
                        context.Entry(packageInclude.Package).State = System.Data.Entity.EntityState.Unchanged;
                        context.Entry(packageInclude).State = System.Data.Entity.EntityState.Added;
                        context.PackageIncludes.Add(packageInclude);
                        context.SaveChanges();
                    }
                    catch (Exception x)
                    {
                        Debug.Write(x.Message);
                    }
                }
        }

        public IEnumerable<Line> GetLinesofClient(Client c)
        {
            using (PhoneCompanyContext context = new PhoneCompanyContext())
            {
                var t = context.Lines.Where(l => l.ClientId == c.Id).ToList();
                return t;
            }
        }

        public IEnumerable<SMS> GetSMSsOfClient(string clintId)
        {
            using (PhoneCompanyContext context = new PhoneCompanyContext())
            {
                List<Line> lines = GetLinesByClientId(clintId).ToList();
                List<SMS> smss = new List<SMS>();
                for (int i = 0; i < lines.Count; i++)
                {
                    //לא עובד!
                    //calls.AddRange(context.Calls.Where(c => c.LineId == lines[i].Id));

                    //כן עובד!
                    foreach (var item in context.SMSs)
                    {
                        if (item.LineId == lines[i].Id)
                        {
                            smss.Add(item);
                        }
                    }
                }
                return smss;
            }

        }

        public IEnumerable<Call> GetCallsOfClient(string clintId)
        {
            using (PhoneCompanyContext context = new PhoneCompanyContext())
            {
                List<Line> lines = GetLinesByClientId(clintId).ToList();
                List<Call> calls = new List<Call>();
                for (int i = 0; i < lines.Count; i++)
                {
                    //לא עובד!
                    //calls.AddRange(context.Calls.Where(c => c.LineId == lines[i].Id));

                    //כן עובד!
                    foreach (var item in context.Calls)
                    {
                        if (item.LineId == lines[i].Id)
                        {
                            calls.Add(item);
                        }
                    }
                }
                return calls;
            }
        }
        public IEnumerable<Call> GetCallsOfLine(int lineId)
        {
            using (PhoneCompanyContext context = new PhoneCompanyContext())
            {
                List<Call> calls = new List<Call>();
                foreach (var item in context.Calls)
                {
                    if (item.LineId == lineId)
                    {
                        calls.Add(item);
                    }
                }
                return calls;
            }
        }
        private bool method(Call c, Line line)
        {
            if (c.LineId == line.Id)
                return true;
            else return false;
        }

        public ClientType GetClientType(Client client)
        {
            using (PhoneCompanyContext context = new PhoneCompanyContext())
            {
                return context.ClientTypes.FirstOrDefault(c => c.Id == client.ClientTypeId);
            }
        }

        #region 

        /*
        //public T ExecuteDbFunction<T>(Func<PhoneCompanyContext, T> functions)
        //{
        //    using (PhoneCompanyContext context = new PhoneCompanyContext())
        //    {
        //        var res = functions(context);
        //        context.SaveChanges();
        //        return res;
        //    }
        //}

        public T ExecuteDbAction(params Func<PhoneCompanyContext>[] actions)
        {
            using (PhoneCompanyContext context = new PhoneCompanyContext())
            {
                foreach (var action in actions)
                {
                    action(context);
                }
                context.SaveChanges();
            }
        }

        public void ExecuteDbAction<T>( Func<PhoneCompanyContext,T> function)
        {
            using (PhoneCompanyContext context = new PhoneCompanyContext())
            {
                
                function(context);
                context.SaveChanges();
            }
        }

        //public void SendCall2(Call call) => ExecuteDbAction((context) => Sendcallwithcontext(context, call));
        public void Sendcallwithcontext(PhoneCompanyContext context, Call call)
        {
            context.Entry(call.Line).State = System.Data.Entity.EntityState.Unchanged;
            context.Entry(call).State = System.Data.Entity.EntityState.Added;
            context.Calls.Add(call);
        }



        public void SendCallANDSMS2(Call call, SMS sms)
        {
            ExecuteDbAction((context) => Sendcallwithcontext(context, call), (context) => Sendcallwithcontext(context, sms));
        }




        private void Sendcallwithcontext(PhoneCompanyContext context, SMS sms)
        {
            //adds sms to context
        }
        */
        #endregion

        public IEnumerable<SMS> GetSMSsOfLine(int lineId)
        {
            using (PhoneCompanyContext context = new PhoneCompanyContext())
            {
                List<SMS> smss = new List<SMS>();
                foreach (var item in context.SMSs)
                {
                    if (item.LineId == lineId)
                    {
                        smss.Add(item);
                    }
                }
                return smss;
            }
        }

        public Package GetPackage(int id)
        {
            using (PhoneCompanyContext context = new PhoneCompanyContext())
            {
                return context.Packages.FirstOrDefault(p => p.Id == id);
            }
        }

        public Client DeleteClient(Client client)
        {
            using (PhoneCompanyContext context = new PhoneCompanyContext())
            {
                return context.Clients.Remove(client);
            }
        }

        public Line DeleteLine(Line line)
        {
            using (PhoneCompanyContext context = new PhoneCompanyContext())
            {
                return context.Lines.Remove(line);
            }
        }

        public Package GetPackage(string name)
        {
            using (PhoneCompanyContext context = new PhoneCompanyContext())
            {
                return context.Packages.FirstOrDefault(p => p.Name == name);
            }
        }
    }
}
