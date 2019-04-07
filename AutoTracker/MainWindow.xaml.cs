using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace AutoTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MemoryReader memAccess;
        Trackables tracked;
        public MainWindow()
        {
            tracked = new Trackables();
            this.DataContext = tracked;
            InitializeComponent();
            ThreadStart method = new ThreadStart(dataUpdater);
            Thread updater = new Thread(method);
            updater.IsBackground = true;
            updater.Start();
        }

        /*private void reconnect(object sender, RoutedEventArgs e) //No longer used
        {
            memAccess.init();
        }/**/

        private void dataUpdater()
        {
            memAccess = new MemoryReader();
            byte[] buffer = new byte[0x7C];
            while (true)
            {
                int check = 0;
                check = memAccess.getBytes(0x9C4, buffer, 2, 0);
                if(check == 2)
                {
                    if((buffer[0] + buffer[1]*256) % 100 != 99)
                    {
                        check = memAccess.getBytes(0x2F5C + 0x9A4, buffer, 2, 1);
                        if (check == 2)
                        {
                            tracked.varia = (buffer[0] & 0x1) != 0;
                            tracked.spring = (buffer[0] & 0x2) != 0;
                            tracked.morph = (buffer[0] & 0x4) != 0;
                            tracked.screw = (buffer[0] & 0x8) != 0;
                            tracked.grav = (buffer[0] & 0x20) != 0;
                            tracked.hjb = (buffer[1] & 0x1) != 0;
                            tracked.space = (buffer[1] & 0x2) != 0;
                            tracked.bomb = (buffer[1] & 0x10) != 0;
                            tracked.speed = (buffer[1] & 0x20) != 0;
                        }
                        check = memAccess.getBytes(0x2F5C + 0x09A8, buffer, 2, 1);
                        if (check == 2)
                        {
                            tracked.wave = (buffer[0] & 0x1) != 0;
                            tracked.ice = (buffer[0] & 0x2) != 0;
                            tracked.spazer = (buffer[0] & 0x4) != 0;
                            tracked.plasma = (buffer[0] & 0x8) != 0;
                            tracked.charge = (buffer[1] & 0x10) != 0;
                        }
                        check = memAccess.getBytes(0x2F5C + 0xD829, buffer, 6, 1);
                        if (check == 6)
                        {
                            tracked.kraid = (buffer[0] & 0x1) == 0;
                            tracked.ridley = (buffer[1] & 0x1) == 0;
                            tracked.croc = (buffer[1] & 0x2) != 0;
                            tracked.phantoon = (buffer[2] & 0x1) == 0;
                            tracked.draygon = (buffer[3] & 0x1) == 0;
                        }
                        check = memAccess.getBytes(0x2F5C + 0xD821, buffer, 1, 1);
                        if (check == 1)
                        {
                            tracked.shak = (buffer[0] & 0x20) != 0;
                        }
                        check = memAccess.getBytes(0xF300, buffer, 0x7C, 0);
                        if(check == 0x7C)
                        {
                            tracked.hookshot = (buffer[0x42] & 0x1) != 0;
                            tracked.mushroom = (buffer[0x44] & 0x1) != 0;
                            tracked.mushroom = (buffer[0x44] & 0x2) != 0;
                            tracked.firerod = (buffer[0x45] & 0x1) != 0;
                            tracked.icerod = (buffer[0x46] & 0x1) != 0;
                            tracked.bombos = (buffer[0x47] & 0x1) != 0;
                            tracked.ether = (buffer[0x48] & 0x1) != 0;
                            tracked.quake = (buffer[0x49] & 0x1) != 0;
                            tracked.lantern = (buffer[0x4A] & 0x1) != 0;
                            tracked.hammer = (buffer[0x4B] & 0x1) != 0;
                            tracked.shovel = (buffer[0x4C] & 0x1) != 0;
                            tracked.flute = (buffer[0x4C] & 0x2) != 0;
                            
                            tracked.book = (buffer[0x4E] & 0x1) != 0;
                            tracked.bottle = (buffer[0x4F] & 0x1) != 0;
                            tracked.somaria = (buffer[0x50] & 0x1) != 0;
                            tracked.byrna = (buffer[0x51] & 0x1) != 0;
                            tracked.cape = (buffer[0x52] & 0x1) != 0;
                            tracked.mirror = (buffer[0x53] & 0x2) != 0;
                            tracked.glove = (buffer[0x54]) != 0;
                            tracked.mitts = (buffer[0x54] & 0x2) != 0;
                            tracked.boots = (buffer[0x55] & 0x1) != 0;
                            tracked.flippers = (buffer[0x56] & 0x1) != 0;
                            tracked.moonpearl = (buffer[0x57] & 0x1) != 0;
                            tracked.silvers = (buffer[0x78] & 0x1) != 0;
                        }
                    }
                    else
                    {
                        check = memAccess.getBytes(0x09A4, buffer, 2, 0);
                        if (check == 2)
                        {
                            tracked.varia = (buffer[0] & 0x1) != 0;
                            tracked.spring = (buffer[0] & 0x2) != 0;
                            tracked.morph = (buffer[0] & 0x4) != 0;
                            tracked.screw = (buffer[0] & 0x8) != 0;
                            tracked.grav = (buffer[0] & 0x20) != 0;
                            tracked.hjb = (buffer[1] & 0x1) != 0;
                            tracked.space = (buffer[1] & 0x2) != 0;
                            tracked.bomb = (buffer[1] & 0x10) != 0;
                            tracked.speed = (buffer[1] & 0x20) != 0;
                        }
                        check = memAccess.getBytes(0x09A8, buffer, 2, 0);
                        if (check == 2)
                        {
                            tracked.wave = (buffer[0] & 0x1) != 0;
                            tracked.ice = (buffer[0] & 0x2) != 0;
                            tracked.spazer = (buffer[0] & 0x4) != 0;
                            tracked.plasma = (buffer[0] & 0x8) != 0;
                            tracked.charge = (buffer[1] & 0x10) != 0;
                        }
                        check = memAccess.getBytes(0xD829, buffer, 6, 0);
                        if (check == 6)
                        {
                            tracked.kraid = (buffer[0] & 0x1) == 0;
                            tracked.ridley = (buffer[1] & 0x1) == 0;
                            tracked.croc = (buffer[1] & 0x2) != 0;
                            tracked.phantoon = (buffer[2] & 0x1) == 0;
                            tracked.draygon = (buffer[3] & 0x1) == 0;
                        }
                        check = memAccess.getBytes(0xD821, buffer, 1, 0);
                        if (check == 1)
                        {
                            tracked.shak = (buffer[0] & 0x20) != 0;
                        }
                        check = memAccess.getBytes(0x3B00, buffer, 0x7C, 1);
                        if (check == 0x7C)
                        {
                            tracked.hookshot = (buffer[0x42] & 0x1) != 0;
                            tracked.mushroom = (buffer[0x44] & 0x1) != 0;
                            tracked.mushroom = (buffer[0x44] & 0x2) != 0;
                            tracked.firerod = (buffer[0x45] & 0x1) != 0;
                            tracked.icerod = (buffer[0x46] & 0x1) != 0;
                            tracked.bombos = (buffer[0x47] & 0x1) != 0;
                            tracked.ether = (buffer[0x48] & 0x1) != 0;
                            tracked.quake = (buffer[0x49] & 0x1) != 0;
                            tracked.lantern = (buffer[0x4A] & 0x1) != 0;
                            tracked.hammer = (buffer[0x4B] & 0x1) != 0;
                            tracked.shovel = (buffer[0x4C] & 0x1) != 0;
                            tracked.flute = (buffer[0x4C] & 0x2) != 0;

                            tracked.book = (buffer[0x4E] & 0x1) != 0;
                            tracked.bottle = (buffer[0x4F] & 0x1) != 0;
                            tracked.somaria = (buffer[0x50] & 0x1) != 0;
                            tracked.byrna = (buffer[0x51] & 0x1) != 0;
                            tracked.cape = (buffer[0x52] & 0x1) != 0;
                            tracked.mirror = (buffer[0x53] & 0x2) != 0;
                            tracked.glove = (buffer[0x54]) != 0;
                            tracked.mitts = (buffer[0x54] & 0x2) != 0;
                            tracked.boots = (buffer[0x55] & 0x1) != 0;
                            tracked.flippers = (buffer[0x56] & 0x1) != 0;
                            tracked.moonpearl = (buffer[0x57] & 0x1) != 0;
                            tracked.silvers = (buffer[0x78] & 0x1) != 0;
                        }
                    }
                }
                else
                {
                    memAccess.init();
                }
                Thread.Sleep(17);
            }
        }
    }

    public class Trackables : INotifyPropertyChanged
    {
        private bool _varia;
        public bool varia
        {
            get { return this._varia; }
            set { Set(value, ref this._varia, "varia"); }
        }
        private bool _spring;
        public bool spring
        {
            get { return this._spring; }
            set { Set(value, ref this._spring, "spring"); }
        }
        private bool _morph;
        public bool morph
        {
            get { return this._morph; }
            set { Set(value, ref this._morph, "morph"); }
        }
        private bool _screw;
        public bool screw
        {
            get { return this._screw; }
            set { Set(value, ref this._screw, "screw"); }
        }
        private bool _grav;
        public bool grav
        {
            get { return this._grav; }
            set { Set(value, ref this._grav, "grav"); }
        }
        private bool _hjb;
        public bool hjb
        {
            get { return this._hjb; }
            set { Set(value, ref this._hjb, "hjb"); }
        }
        private bool _space;
        public bool space
        {
            get { return this._space; }
            set { Set(value, ref this._space, "space"); }
        }
        private bool _bomb;
        public bool bomb {
            get { return this._bomb; }
            set { Set(value, ref this._bomb, "bomb"); }
        }
        private bool _speed;
        public bool speed
        {
            get { return this._speed; }
            set { Set(value, ref this._speed, "speed"); }
        }
        private bool _grapple;
        public bool grapple
        {
            get { return this._grapple; }
            set { Set(value, ref this._grapple, "grapple"); }
        }
        private bool _xray;
        public bool xray
        {
            get { return this._xray; }
            set { Set(value, ref this._xray, "xray"); }
        }
        private bool _charge;
        public bool charge
        {
            get { return this._charge; }
            set { Set(value, ref this._charge, "charge"); }
        }
        private bool _spazer;
        public bool spazer
        {
            get { return this._spazer; }
            set { Set(value, ref this._spazer, "spazer"); }
        }
        private bool _ice;
        public bool ice
        {
            get { return this._ice; }
            set { Set(value, ref this._ice, "ice"); }
        }
        private bool _wave;
        public bool wave
        {
            get { return this._wave; }
            set { Set(value, ref this._wave, "wave"); }
        }
        private bool _plasma;
        public bool plasma
        {
            get { return this._plasma; }
            set { Set(value, ref this._plasma, "plasma"); }
        }
        private bool _kraid;
        public bool kraid
        {
            get { return this._kraid; }
            set { Set(value, ref this._kraid, "kraid"); }
        }
        private bool _draygon;
        public bool draygon
        {
            get { return this._draygon; }
            set { Set(value, ref this._draygon, "draygon"); }
        }
        private bool _phantoon;
        public bool phantoon
        {
            get { return this._phantoon; }
            set { Set(value, ref this._phantoon, "phantoon"); }
        }
        private bool _ridley;
        public bool ridley
        {
            get { return this._ridley; }
            set { Set(value, ref this._ridley, "ridley"); }
        }
        private bool _croc;
        public bool croc
        {
            get { return this._croc; }
            set { Set(value, ref this._croc, "croc"); }
        }
        private bool _shak;
        public bool shak
        {
            get { return this._shak; }
            set { Set(value, ref this._shak, "shak"); }
        }
        private bool _hookshot;
        public bool hookshot
        {
            get { return this._hookshot; }
            set { Set(value, ref this._hookshot, "hookshot"); }
        }
        private bool _cape;
        public bool cape
        {
            get { return this._cape; }
            set { Set(value, ref this._cape, "cape"); }
        }
        private bool _bottle;
        public bool bottle
        {
            get { return this._bottle; }
            set { Set(value, ref this._bottle, "bottle"); }
        }
        private bool _ether;
        public bool ether
        {
            get { return this._ether; }
            set { Set(value, ref this._ether, "ether"); }
        }
        private bool _firerod;
        public bool firerod
        {
            get { return this._firerod; }
            set { Set(value, ref this._firerod, "firerod"); }
        }
        private bool _flippers;
        public bool flippers
        {
            get { return this._flippers; }
            set { Set(value, ref this._flippers, "flippers"); }
        }
        private bool _flute;
        public bool flute
        {
            get { return this._flute; }
            set { Set(value, ref this._flute, "flute"); }
        }
        private bool _glove;
        public bool glove
        {
            get { return this._glove; }
            set { Set(value, ref this._glove, "glove"); }
        }
        private bool _hammer;
        public bool hammer
        {
            get { return this._hammer; }
            set { Set(value, ref this._hammer, "hammer"); }
        }
        private bool _icerod;
        public bool icerod
        {
            get { return this._icerod; }
            set { Set(value, ref this._hammer, "icerod"); }
        }
        private bool _lantern;
        public bool lantern {
            get { return this._lantern; }
            set { Set(value, ref this._lantern, "lantern"); }
        }
        private bool _mirror;
        public bool mirror
        {
            get { return this._mirror; }
            set { Set(value, ref this._mirror, "mirror"); }
        }
        private bool _moonpearl;
        public bool moonpearl
        {
            get { return this._moonpearl; }
            set { Set(value, ref this._moonpearl, "moonpearl"); }
        }
        private bool _mushroom;
        public bool mushroom
        {
            get { return this._mushroom; }
            set { Set(value, ref this._mushroom, "mushroom"); }
        }
        private bool _powder;
        public bool powder
        {
            get { return this._powder; }
            set { Set(value, ref this._powder, "powder"); }
        }
        private bool _quake;
        public bool quake
        {
            get { return this._quake; }
            set { Set(value, ref this._quake, "quake"); }
        }
        private bool _shovel;
        public bool shovel
        {
            get { return this._shovel; }
            set { Set(value, ref this._shovel, "shovel"); }
        }
        private bool _silvers;
        public bool silvers
        {
            get { return this._silvers; }
            set { Set(value, ref this._silvers, "silvers"); }
        }
        private bool _somaria;
        public bool somaria
        {
            get { return this._somaria; }
            set { Set(value, ref this._somaria, "somaria"); }
        }
        private bool _sword;
        public bool sword
        {
            get { return this._sword; }
            set { Set(value, ref this._sword, "sword"); }
        }
        private bool _bombos;
        public bool bombos
        {
            get { return this._bombos; }
            set { Set(value, ref this._bombos, "bombos"); }
        }
        private bool _book;
        public bool book
        {
            get { return this._book; }
            set { Set(value, ref this._book, "book"); }
        }
        private bool _boots;
        public bool boots
        {
            get { return this._boots; }
            set { Set(value, ref this._boots, "boots"); }
        }
        private bool _byrna;
        public bool byrna
        {
            get { return this._byrna; }
            set { Set(value, ref this._byrna, "byrna"); }
        }
        private bool _mitts;
        public bool mitts
        {
            get { return this._mitts; }
            set { Set(value, ref this._mitts, "mitts"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private string _title;
        public string title
        {
            get { return this._title; }
            set {
                this._title = value;
                RaisePropertyChanged("title");
            }
        }
        public Trackables()
        {
            _title = "Super Metroid Automatic Tracker";
            _varia = false;
            _spring = false;
            _morph = false;
            _screw = false;
            _grav = false;
            _hjb = false;
            _space = false;
            _bomb = false;
            _speed = false;
            _grapple = false;
            _xray = false;
            _charge = false;
            _spazer = false;
            _ice = false;
            _wave = false;
            _plasma = false;
        }
        private void Set(bool value, ref bool cur, string propName)
        {
            if(value != cur)
            {
                cur = value;
                RaisePropertyChanged(propName);
            }
        }
    }
}