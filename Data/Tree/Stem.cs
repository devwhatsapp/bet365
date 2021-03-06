﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Bet365.Data.Tree
{
    public class Stem :ILookupClient
    {

        public DateTime Time { get; set; }
        
        public Stem()
        {
          //  this.Location = _location;
        }

        #region 接口方法
         Dictionary<string, object> ILookupClient.data
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

         bool ILookupClient.hasLookupReferenct
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        #region AS定义 ，注释
        /*
       // protected object _data;
         protected Dictionary<string, object> _data;
        System.Collections.ArrayList _acturalchildren;
        protected string _nodename;
        protected bool _hasLookupReference;
        protected Array _children;
        protected int numListeners = 0;
        protected bool _hasListeners = false;

        protected Stem parent;
        bool _filtered;
        protected bool filterInvalidated;
        public static string FilterToken;
        static TreeLookup lookup = new TreeLookup();//TODO: 这里as代码里面用的是单例，目前不管它

        public Stem()
        {
            _acturalchildren = new System.Collections.ArrayList();

        }

        public virtual void insert(object p1, Type p2 = null)
        {
            //var loc3=new object();
            Stem loc3 = new Stem();
            //var loc5=new object();
            DataMessage loc5 = new DataMessage();
          //  p2 = p2 || Stem;
            if (p1 is DataMessage)
            {
                loc5 = p1 as DataMessage;
                //loc3=new p2;
                loc3 = new Stem();

               // loc3._data = loc5.item;
                loc3._nodename = loc5.nodeType;
            }
            else if (p1 is Stem)
            {
                loc3 = p1 as Stem;
            }
            else
            {
                loc3 = new Stem();
                //loc3._data = p1;
            }

            loc3.parent = this;
            object loc4 = loc3._data["OR"];
            if (loc3._data["OR"]!=null)
            {
                //_acturalchildren.InsertRange(
            }
            else
            {
                _acturalchildren[_acturalchildren.Count] = loc3;
            }
            if (_children!=null && !loc3._filtered)
            {

            }
            if (_hasListeners && !loc3._filtered)
            {
                //dispatchEvent
            }

        }

        public virtual void remove()
        {

        }


        bool HasListeners
        {
            get { return _hasListeners; }
        }

        public void update(object p1)
        {
        }

        public bool Filtered
        {
            get { return _filtered; }
        }

        public void adoptStem(Stem p1)
        {

        }

        public string NodeName
        {
            set { _nodename = value; }
            get { return _nodename; }
        }


        public void adope(Array p1)
        {
        
        }

        public Array Children
        {
            get { return _children; }
        }

        public Dictionary<string,object> Data
        {
            get;
            set;
        }
         */
        #endregion

         #region 属性

        #region Line
         private string _line;

         /// <summary>
         /// 行原始数据
         /// </summary>
         public string Line
         {
             get { return _line; }
             set { _line = value; }
         }
        #endregion

         #region Name
         /// <summary>
        /// 信息行(单元格)名称 如 MA,CO,PA等
        /// </summary>
         public string Name
         {
             get;
             set;
         }
        #endregion


        //TODO: 这里设置Parent要多调几次 ！！！！
         #region Parent


         private Stem _parent;
        /// <summary>
         /// 父节点,这里自动装载Childrens和l_Childrens
        /// </summary>
         public Stem Parent
         {
             get { return _parent; }
             set
             {
                 //TODO: 这里还要处理一种情况可能是整个child覆盖的，不是一条条更新的
                 _parent = value;
                 string key = "";
                 try
                 {

                     //value.Childrens.Add(value.Properties["IT"], this);
                     if (this.Name.Contains("InPlay_10_0"))
                     {
                         //value.Childrens.Add("Inplay_10_0", this);
                         key = "InPlay_10_0";
                     }
                     else
                     {
                        // value.Childrens.Add(this.Properties["IT"], this);
                         key = this.Properties["IT"];
                     }
                     //value.l_Childrens.Add(this);

                         foreach (Stem temchild in value.Childrens.Values)
                         {
                             if (temchild.Properties["ID"] == this.Properties["ID"] || temchild.Properties["IT"] == this.Properties["IT"])
                             {
                                 try
                                 {
                                     Console.WriteLine("有相同的Stem,删除！！");
                                     value.l_Childrens.Remove(temchild);
                                     value.Childrens.Remove(temchild.Properties["IT"]);
                                     break;
                                 }
                                 catch (Exception ex)
                                 {
                                     Console.WriteLine(ex.ToString());
                                 }
                             }
                         }
                     


                     if (value.Childrens.ContainsKey(key))
                     {
                         Console.WriteLine("已经有相同的键,请检查");
                     }
                     else
                     {
                         value.Childrens.Add(key, this);
                         if (!value.l_Childrens.Contains(this))
                         {
                             value.l_Childrens.Add(this);
                         }
                         
                     }
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine(ex.ToString());
                 }

                 //ts.Parent.Children.Add(ts.Properties["IT"],ts);

                // value.l_Childrens.Add(this);
                 
             }
         }
         #endregion

         #region Location
         private UI.Location _location = new UI.Location();
        /// <summary>
        /// 坐标
        /// </summary>
         public UI.Location Location
         {
             get { return _location; }
             set
             {
                 this._location = value;
              //   Console.WriteLine(value.X + "" + value.Y);
             }
         }
        #endregion

         #endregion

         #region 委托
         public delegate void HandlePaint(string x, string y, string data);

         public delegate void HandleUpdateDataGridView(object sender, object valueContent, object value);

         public event HandleUpdateDataGridView RepaintValue;

         public event HandlePaint RepaintCell;


         void Draw(string x, string y, string data)
         {
             if (RepaintCell != null)
             {
                 RepaintCell(x, y, data);
             }
         }

         void Reflesh(object sender, object valueContent, object value)
         {
             if (RepaintValue != null)
             {
                 RepaintValue(sender, valueContent, value);
             }
         }

         #endregion


         public Dictionary<string, Stem> Childrens = new Dictionary<string, Stem>();

         public List<Stem> l_Childrens = new List<Stem>();

         /// <summary>
         /// 这里存储每个行列(单元格信息)的所有属性，如NA,IT,ID等
         /// </summary>
         public StemProperty Properties = new StemProperty();
         //public Dictionary<string, string> Properties = new Dictionary<string, string>();

         //public Dictionary<string, string> Properties
         //{
         //    get { return _properties; }
         //    set { _properties = value; 
             
         //    }
         //}

        /// <summary>
        /// 根据IT删除子Stem
        /// </summary>
        /// <param name="it"></param>
         public void RemoveChildStem(string it)
         {
             var dc = Childrens[it];
             if (dc == null) return;



             Childrens.Remove(it);

           //  l_Childrens.Remove(dc);//实际以Childrens为准 l_Childrens可以保存已经删除的历史数据
            
             Console.WriteLine("删除 {0} {1} ",this.Name,it);
         }


        //public Stem GetChildsByTagName(string 
        /// <summary>
        ///     2015/11/30
        ///     用递归方式查询该节点一下全部分支符合条件的第一个子节点
        ///     例如 : ID = 222222
        ///     
        /// </summary>
        /// <param name="p">属性名</param>
        /// <param name="v">属性值</param>
        /// <returns>没有找到则返回NULL</returns>
         public Stem FindChildByProperty(string p,string v)
         {
             Stem tem_s = this.l_Childrens.Find(c => (c.Properties.ContainsKey(p) && c.Properties[p] == v));
             //if (tem_s != null)
             if (tem_s == null)
             {
                // foreach (var cc in tem_s.l_Childrens)
                 foreach (var cc in this.l_Childrens)
                 {
                     var  fcs = cc.FindChildByProperty(p, v);
                     if (fcs != null)
                     {
                         return fcs;
                     }
                 }
             }

             return tem_s;
         }

         public override string ToString()
         {
             if (this.Name == "ST")
             {
                 return this.Properties["LA"];
             }
             return _line;
         }





    }

}
