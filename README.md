# AirFighter

# Објаснување на проблемот #

**AirFighter** претстатува едноставна 2Д игра во која целта е да уништиме што е можно повеќе непријателски авиони кои ке ни се најдат на патот т.е не смееме да дозволиме да се разминеме со нив бидејки со тоа ние губиме живот. 
Идејата за играта е од веќе постоечки игри од овој карактер, како што се ``` Mig 2D: Retro Shooter ```, ``` Alpha Combat: Air Shooter 1945 ``` и уште некои слични, меѓутоа целата замисла и имплентација на самата игра е потполно наша без да користиме веќе постоечки кодови.

# Опис на решението на проблемот #

За имплентација на играта дефиниравме неколку класи. Така, за претставување на авионите дефиниравме абстрактна класа **SpaceShip**, која ке биде имплементирана од страна на класата **PlayerShip** која го репрезентира нашиот авион и класата за ререзентација на противничките авиони т.е **EnemyShip**. За репрезентација на истрелот кој го упатува нашиот авион ја дефинираме класата **Bullet**. 

Во класата **Scene** се чуваат податоците кои што ни се потребни за реализација на самата игра. Тоа се листа на непријателски авиони, листа на истрели, самиот играч, информации за бројот на поени како и за преостанатите животи,
слики за позадината на играта, потоа тајмери за придвижување на елементите на екранот, листа од досегашните најдобри остварени резултати, како и мелодии кои го репрезентираат аудио делот од играта. Во оваа класа се имплементирани најголемиот дел од функционалностите на играта, при што секоја функционалност е накратко објаснета.

Исто така е дефинирана **Serializable** класата **ScoreBoardEntry** со чии објекти се претставуваат резултатите од една одиграна игра.

Дополнително, формата **ScoreBoardForm** ја користиме за евидентирање на играчот односно преку оваа форма играчот го внесува името под кое сака да биде запишан неговиот остварен резултат.

                                                ScoreBoardForm
<p align="center"><img src="https://scontent-vie.xx.fbcdn.net/hphotos-xta1/v/t34.0-12/11262265_913401548701514_548318271_n.jpg?oh=4a64e205492d5e2b45c1374bc4d76f0f&oe=55514B68">
</p>

# Опис на класа од изворниот код на проектот #

Во прилог е даден опис на класата Bullet која го репрезентира истрелот од нашиот авион.

    /// <summary>
    /// Klasa vo koja se chuvaat osnovnite podatoci za oruzhjeto.
    /// </summary>
    public class Bullet {
        private Image BulletImage;
        public Point Position;
        public bool Active;

        /// <summary>
        /// Konstruktor za metakot.
        /// </summary>
        /// <param name="P">Predefinirana tochka od kade kje se pojavi objektot, po default kaj igrachot.</param>
        public Bullet(Point P) {
            BulletImage = Resources.smallBullet;
            Position = new Point(P.X + 23, P.Y - 5);
            Active = true;
        }

        /// <summary>
        /// Pridvizhuvanje na metakot.
        /// </summary>
        public void Move() {
            Position = new Point(Position.X, Position.Y - 40);
        }

        /// <summary>
        /// Iscrtuvanje na metakot.
        /// </summary>
        public void Draw(Graphics g) {
            g.DrawImage(BulletImage, Position);
        }
    }

***На истиот овој начин опис е даден за сите преостанати класи кои се дефинирани во играта. Докуменацијата со описот за функционалностите генерирана од ```VisualStudio``` е дадена во прилог: [https://www.dropbox.com/s/klbvlwzwda8pu2z/AirFighter.xml?dl=0](https://www.dropbox.com/s/klbvlwzwda8pu2z/AirFighter.xml?dl=0 "Documentation").***

#Кратко упатство за играта#
- Почетното мени се состои од 3 полиња, поле за стартување на самата игра, поле со најдобрите остварени резултати и поле со инструкции за начинот на играње на самата игра.

            Start_menu                                          Main_menu
![menu](https://scontent-fra.xx.fbcdn.net/hphotos-xpt1/v/t34.0-12/11251611_913367795371556_1742368023_n.jpg?oh=6075df1962c08fa0df144b07e4596006&oe=55528208)


- На главното мени имаме увид за состојбата на нашите животи кои се дадени во горниот десен агол, како и за бројот на освоени поени кој е даден во горниот лев агол.

- Авионот го управуваме со движење на маусот и притоа го движиме во насока лево или насока десно, додека истрелите кон непријателите ги упатуваме со клик на маусот(било тоа да е лев или десен лик). Притоа гледаме да ги уништиме сите непријатели кои што ке ни се најдат на патот бидејки секој не уништен авион кој ке не одмине ни одзема 1 живот, а ние имаме само 10 животи.


