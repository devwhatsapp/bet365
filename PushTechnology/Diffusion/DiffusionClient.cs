﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Bet365.PushTechnology.Diffusion
{
    public class DiffusionClient
    {
        private DiffusionSocket ds;

        public event EventHandler<EventArgs> onInplayMsg;

        public event EventHandler<EventArgs> onMatchMsg;

        /// <summary>
        /// 改事件仅做断连通知用途，不传递任何信息
        /// </summary>
        public event EventHandler<EventArgs> onDisConnect;

        public string SessionID { get; set; }
        /// <summary>
        /// 获取目标赛事或全部比赛列表标识 
        /// </summary>
        public string TargetTopic { get; set; }

        /// <summary>
        /// 设置是获取赛事还是赛事列表
        /// </summary>
        public TargetType Target { get; set; }

        //public DiffusionClient(string targettopic)
        //{
        //    ds = new DiffusionSocket("5.226.180.15", 843, TargetTopic);
        //    ds.sessionID = SessionID;
        //    ds.onConnectSuccess += new EventHandler<EventArgs>(ds_onConnectSuccess);
        //    ds.onSocketError += new EventHandler<EventArgs>(ds_onSocketError);
        //}


        public void BeginConnect()
        {
            ds = new DiffusionSocket("5.226.180.15", 843, TargetTopic);
            ds.sessionID = SessionID;
            ds.onConnectSuccess += new EventHandler<EventArgs>(ds_onConnectSuccess);
            ds.onSocketError += new EventHandler<EventArgs>(ds_onSocketError);
            ds.Connect();
        }


        void ds_onSocketError(object sender, EventArgs e)
        {
            if (onDisConnect != null)
            {
                onDisConnect(sender, e);
            }
        }

        void ds_onConnectSuccess(object sender, EventArgs e)
        {
            switch (Target)
            {
                case TargetType.Inplay:

                //    ds.onMessage += new EventHandler<EventArgs>(ds_onInplayMessage);
                    ds.onInplayMessage+=new EventHandler<EventArgs>(ds_onInplayMessage);
                    ds_onInplayMessage(sender, e);
                    break;
                case TargetType.Match:
                  //  ds.onMessage += new EventHandler<EventArgs>(ds_onMatchMessage);
                    ds.onMatchMessage+=new EventHandler<EventArgs>(ds_onMatchMessage);
            //        ds_onMatchMessage(sender, e);
                    break;
                default:
                    break;
            }
        }

        void ds_onMatchMessage(object sender, EventArgs e)
        {
            if (onMatchMsg != null)
            {
                onMatchMsg(this, e);
            }
        }

        void ds_onInplayMessage(object sender, EventArgs e)
        {
            if (onInplayMsg != null)
            {
                onInplayMsg(this, e);
            }
        }




        void GetMatchLive()
        {

        }


        public void Close()
        {
            try
            {
                this.ds.Close();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
           
        }



        




    }
}
