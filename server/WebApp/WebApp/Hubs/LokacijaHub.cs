using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using WebApp.Models;
using WebApp.Persistence;

namespace WebApp.Hubs
{
    [HubName("notificationsBus")]
    public class LokacijaHub : Hub
    {
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<LokacijaHub>();



        private static List<Stanica> stations = new List<Stanica>();

        private static Timer timer = new Timer();
        private static int cnt = 0;
        private static int broj = 0;
        private static int unazad = 0;

        public LokacijaHub()
        {
        }

        public void TimeServerUpdates()
        {
            //if (!timer.Enabled)
            //{ 
            //timer = new Timer();
            if (timer.Interval != 2000)
            {

                timer.Interval = 2000;
                //timer.Start();
                timer.Elapsed += OnTimedEvent;
            }

            timer.Enabled = true;
            //}
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
#if DEBUG 
            (source as Timer).Enabled = false;
#endif
            //GetTime();
            if (stations != null)
            {
                if (cnt >= stations.Count)
                {
                    unazad = -1;
                    cnt = stations.Count-1;
                }
                else if(cnt>=0 && unazad == 0)
                {
                    if (broj == 0)
                    {
                        double[] niz = { stations[cnt].GeografskeKoordinataX, stations[cnt].GeografskeKoordinataY };
                        hubContext.Clients.All.setRealTime(niz);
                        //Clients.All.SendAsync("setRealTime", niz);
                        cnt++;
                        broj++;
                    }
                    else
                    {
                        if (cnt < stations.Count - 1)
                        {
                        double b1 = (stations[cnt].GeografskeKoordinataX + stations[cnt + 1].GeografskeKoordinataX) / 2;
                        double b2 = (stations[cnt].GeografskeKoordinataY + stations[cnt + 1].GeografskeKoordinataY) / 2;

                        double[] niz = { b1, b2 };
                        hubContext.Clients.All.setRealTime(niz);
                        //Clients.All.SendAsync("setRealTime", niz);
                        cnt++;
                        broj=0;
                        }
                        else
                        {
                            double[] niz = { stations[cnt].GeografskeKoordinataX, stations[cnt].GeografskeKoordinataY };
                            hubContext.Clients.All.setRealTime(niz);
                            //Clients.All.SendAsync("setRealTime", niz);
                            cnt++;
                            broj=0;
                        }
                    }
                    
                }
                else if (cnt!=0 && unazad == -1)
                {

                    if (broj == 0)
                    {
                        double[] niz = { stations[cnt].GeografskeKoordinataX, stations[cnt].GeografskeKoordinataY };
                        hubContext.Clients.All.setRealTime(niz);
                        //Clients.All.SendAsync("setRealTime", niz);
                        cnt--;
                        broj++;
                    }
                    else
                    {
                        double b1 = (stations[cnt].GeografskeKoordinataX + stations[cnt - 1].GeografskeKoordinataX) / 2;
                        double b2 = (stations[cnt].GeografskeKoordinataY + stations[cnt - 1].GeografskeKoordinataY) / 2;

                        double[] niz = { b1, b2 };
                        hubContext.Clients.All.setRealTime(niz);
                        //Clients.All.SendAsync("setRealTime", niz);
                        cnt--;
                        broj = 0;
                    }

                    if (cnt==0)
                    {
                        unazad = 0;
                    }
                }

            }
            else
            {
                double[] nizz = { 0, 0 };
                //Clients.All.SendAsync("setRealTime", nizz);
                //Clients.All.setRealTime(nizz);
            }
#if DEBUG
            (source as Timer).Enabled = true;
#endif
        }

        public void GetTime()
        {
            if (stations.Count > 0)
            {
                if (cnt >= stations.Count)
                {
                    cnt = 0;
                }
                double[] niz = { stations[cnt].GeografskeKoordinataX, stations[cnt].GeografskeKoordinataY };
                //Clients.All.setRealTime(niz);

                cnt++;
            }
        }

        public void StopTimeServerUpdates()
        {
            timer.Stop();
            stations = null;
        }

        public void AddStations(List<Stanica> stationsBM)
        {
            stations = new List<Stanica>();
            stations = stationsBM;
            //TimeServerUpdates();
        }

        private ApplicationDbContext db = new ApplicationDbContext();
        

        public void NotifyAdmins(int clickCount)
        {
            hubContext.Clients.Group("Admins").userClicked($"Clicks: {clickCount}");
        }

        public override Task OnConnected()
        {
            //if (Context.User.IsInRole("Admin"))
            //{
            Groups.Add(Context.ConnectionId, "Admins");
            //}

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            //if (Context.User.IsInRole("Admin"))
            //{
            Groups.Remove(Context.ConnectionId, "Admins");
            //}

            return base.OnDisconnected(stopCalled);
        }
    }
}