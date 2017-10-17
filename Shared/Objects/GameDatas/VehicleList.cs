﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Shared.Objects.GameDatas
{
    [Serializable]
    [XmlRoot(ElementName = "Vehicles")]
    public class VehicleList
    {
        public class VehicleUpgrade
        {
            // Vehicle upgrade
            [XmlAttribute("coupon")] public string Coupon;
            [XmlAttribute("accel")] public string Acceleration;
            [XmlAttribute("speed")] public string Speed;
            [XmlAttribute("crash")] public string Crash;
            [XmlAttribute("boost")] public string Boost;
            [XmlAttribute("price")] public string Price;

            [XmlAttribute("sell")] public string Sell;
            [XmlAttribute("closeSell")] public string CloseSell;
            [XmlAttribute("upgradeMito")] public string UpgradeMito;
            
            /// <summary>
            /// Fuel Efficiency
            /// </summary>
            [XmlAttribute("efficiency")] public string Efficiency;
            
            /// <summary>
            /// Fuel Capacity
            /// </summary>
            [XmlAttribute("capacity")] public string Capacity;

            [XmlAttribute("reqLevel")] public string RequiredLevel;         
        }
        
        public class VehicleData
        {
            [XmlAttribute("name")] public string Name;
            
            // PlayerCar, HUV, Traffic, RacingBattle
            /// <summary>
            /// The type of the vehicle
            /// Possible values:
            /// 0 = Player Car
            /// 1 = HUV
            /// 2 = Traffic
            /// 3 = Racing Battle
            /// </summary>
            [XmlAttribute("type")] public string Type;
            
            // Not sure if we need the string :/.
            [XmlAttribute("typeS")] public string TypeStr;
            
            [XmlAttribute("id")] public string UniqueId;

            /// <summary>
            /// 0 or 1 wether this car can be sold.
            /// </summary>
            [XmlAttribute("sellable")] public string Sellable;

            /// <summary>
            /// Grade typ
            /// </summary>
            [XmlAttribute("grade")] public string Grade;

            /// <summary>
            /// Car Base Acceleration
            /// </summary>
            [XmlAttribute("accel")] public string Acceleration;

            /// <summary>
            /// Car Base Speed
            /// </summary>
            [XmlAttribute("speed")] public string Speed;
            
            /// <summary>
            /// Car Base Crash
            /// </summary>
            [XmlAttribute("crash")] public string Crash;
            
            /// <summary>
            /// Car Base Boost
            /// </summary>
            [XmlAttribute("boost")] public string Boost;

            [XmlAttribute("reqLevel")] public string RequiredLevel;
            [XmlAttribute("level")] public string Level;


            [XmlElement(ElementName = "Upgrade")]
            public List<VehicleUpgrade> Upgrades;
        }
        
        [XmlElement(ElementName = "Vehicle")]
        public List<VehicleData> VehList = new List<VehicleData>();

        public void Save(string fileName)
        {
            var serializer = new XmlSerializer(typeof(VehicleList));
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            using (var writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, this, ns);
            }
        }
    }
}