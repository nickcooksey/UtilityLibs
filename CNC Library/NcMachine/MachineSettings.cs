using System;
using System.Collections.Generic;
using ICNCLib;

namespace CNCLib
{

    /// <summary>
    /// CNC and PLC string values from file machineSettings.xml
    /// </summary>
    ///     
    public class MachineSettings
    {
        /// <summary>
        /// IP address of CNC machine
        /// </summary>
        public string IP { get; set; }
        public string ProgramDirectory { get; set; }
        public UInt32 Port { get; set; }

        public long TimeOutMs { get; set; }



        public ControllerType Controller { get; set; }
        public string Name { get; set; }
        public int AxisCount { get; set; }
        public List<Axis> Axes { get; set; }
        public MachineGeometry MachineGeometry { get; set; }

        public string StartInspectionTrigger { get; set; }
        public string InPositionTrigger { get; set; }
        public double AngleEpsilon { get; set; }
        public double MsPerReading { get; set; }
        public double LengthEpsilon { get; set; }

        public bool CncIsAttached { get; set; }
        public string NCProgExtension { get; set; }

        public MachineSettings()
        {
            IP = "192.168.0.102";
            Port = 0;
            ProgramDirectory = "ftp://192.168.0.102/disk/prg";
            TimeOutMs = 10000;

            Axes = new List<Axis>();



            MachineGeometry = MachineGeometry.XYZBC;

            StartInspectionTrigger = "o5";
            InPositionTrigger = "o5";
            AngleEpsilon = 0.001;
            MsPerReading = 50;
            LengthEpsilon = 0.0001;

            CncIsAttached = false;
            NCProgExtension = ".nc";
        }
    }
}
