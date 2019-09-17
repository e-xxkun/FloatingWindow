
using System;
using System.Collections;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace FloatingWindow
{
	public class Monitor
	{
		private Network network;
		private Cpu cpu;
		private Ram ram;
		private Disk disk;
		
		public Monitor()
		{
			PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface");
			foreach (string name in category.GetInstanceNames())
			{
				if (name == "MS TCP Loopback interface")
					continue;
				Network n = new Network(name);
				if(n.isConnect){
					network=n;
					break;
				}
			}
			cpu=new Cpu();
			ram=new Ram();
			disk=new Disk();
		}
		
		public void refresh(){
			network.refresh();
			cpu.refresh();
			ram.refresh();
			disk.refresh();
		}
		
		public string getDownSpeed(){
			return network.getFormatDownSpeed();
		}
		
		public string getUpSpeed(){
			return network.getFormatUpSpeed();
		}
		
		public int getCpuUtil(){
			return cpu.curValue;
		}
		
		public int getRamUtil(){
			return ram.curValue;
		}
		
		public int getDiskUtil(){
			return disk.curValue;
		}
		
		public bool isDownSpeedChange(){
			return network.isDownSpeedChange;
		}
		
		public bool isUpSpeedChange(){
			return network.isUpSpeedChange;
		}
		
		public bool isCpuUtilChange(){
			return cpu.curValue!=cpu.oldValue;
		}
		
		public bool isRamUtilChange(){
			return ram.curValue!=ram.oldValue;
		}
		
		public bool isDiskUtilChange(){
			return disk.curValue!=disk.oldValue;
		}
	}
	
	public class Network{
		
		public string name { get; private set; }
		public long downSpeed{ get; private set;}
		public long upSpeed{ get; private set; }
		public bool isDownSpeedChange{ get; private set; }
		public bool isUpSpeedChange{ get; private set; }
		public bool isConnect{ get; private set; }
		
		private PerformanceCounter downCounter, upCounter;
		private long downValue, upValue;
		private long downValueOld, upValueOld;
		private long downSpeedOld,upSpeedOld;
		
		
		public Network(string name){
			this.name=name;
			downCounter	= new PerformanceCounter("Network Interface", "Bytes Received/sec", name);
			upCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", name);
			if(downCounter==null||upCounter==null)
				return;
			downValueOld=downCounter.NextSample().RawValue;
			upValueOld=upCounter.NextSample().RawValue;
			if(downSpeedOld>0||upValueOld>0)isConnect=true;
			else isConnect=false;
		}
		
		public void refresh()
		{
			if(downCounter==null||upCounter==null){
				downSpeed=0;
				upSpeed=0;
				return;
			}
			if(downCounter.NextSample()==null||upCounter.NextSample()==null){
				downSpeed=0;
				upSpeed=0;
				return;
			}
			
			downValue = downCounter.NextSample().RawValue;
			upValue = upCounter.NextSample().RawValue;
			
			downSpeed = downValue - downValueOld;
			upSpeed	= upValue - upValueOld;

			isDownSpeedChange=(downSpeed!=downValue);
			isUpSpeedChange=(upSpeed!=upSpeedOld);
			downSpeedOld=downSpeed;
			upSpeedOld=upSpeed;
			downValueOld=downValue;
			upValueOld=upValue;
		}
		
		public string getFormatDownSpeed(){
			return format(downSpeed);
		}
		
		public string getFormatUpSpeed(){
			return format(upSpeed);
		}
		
		private string format(long num){
			double n=num/1024.0;
			if(n>=1000.0)return (n/1024).ToString("0.00")+"MB/s";
			else if(n>=100.0)return n.ToString("0.0")+"KB/s";
			else return n.ToString("0.00")+"KB/s";
		}
	}
	
	public class Cpu{
		
		public int curValue { get; private set; }
		public int oldValue { get; private set; }

		private PerformanceCounter counter;
				
		public Cpu(){
			counter = new PerformanceCounter("Processor", "% Processor Time","_Total");  
			curValue=(int)counter.NextValue();			
		}
		
		public void refresh(){
			oldValue=curValue;
			curValue=(int)counter.NextValue();
		}
	}
	
	public class Ram{
		
		public struct MEMORYSTATUS
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public ulong dwTotalPhys;
            public ulong dwAvailPhys;
            public ulong dwTotalPageFile;
            public ulong dwAvailPageFile;
            public ulong dwTotalVirtual;
            public ulong dwAvailVirtual;
            public ulong dwAvailExtendedVirtual;
        }
		
		public int curValue { get; private set; }
		public int oldValue { get; private set; }
		
		private MEMORYSTATUS mStatus;
		
		[DllImport("kernel32.dll")]
		public static extern void GlobalMemoryStatus(ref MEMORYSTATUS stat);
		
		public Ram(){
		    GlobalMemoryStatus(ref mStatus);
		    curValue=(int)(mStatus.dwMemoryLoad);
		}
		
		public void refresh(){
			oldValue=curValue;
			GlobalMemoryStatus(ref mStatus);
			curValue=(int)(mStatus.dwMemoryLoad);
		}
	}
	
	public class Disk{
		
		public int curValue { get; private set; }
		public int oldValue { get; private set; }
		
		private PerformanceCounter counter;
		
		public Disk(){
			counter=new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
			int num=(int)counter.NextValue();
			curValue=num>100?100:num;
		}
		
		public void refresh()
		{
			oldValue=curValue;
			int num=(int)counter.NextValue();
			curValue=num>100?100:num;
		}
	}
}
