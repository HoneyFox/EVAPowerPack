using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EVAPowerPack
{
	[KSPAddon(KSPAddon.Startup.MainMenu, false)]
	public class EVAPowerPack : MonoBehaviour
	{
		public static EVAPowerPack s_Singleton = null;

		public Vessel lastEVA = null;
		public bool powerPackEnabled = false;

		public void Awake()
		{
			if(s_Singleton == null)
			{
				s_Singleton = this;
				DontDestroyOnLoad(s_Singleton);
			}
		}

		public void Start()
		{
			MonoBehaviour.print("EVA Power Pack Started!");
		}

		public void Update()
		{
			if (s_Singleton != this) return;

			if (FlightGlobals.fetch != null && FlightGlobals.fetch.activeVessel != null)
			{
				if (lastEVA == FlightGlobals.fetch.activeVessel)
				{
					// Handle input here.
					if (Input.GetKey(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.P))
					{
						powerPackEnabled = !powerPackEnabled;
						if (powerPackEnabled == true)
							ScreenMessages.PostScreenMessage("Power Pack Activated", 3, ScreenMessageStyle.UPPER_CENTER);
						else
							ScreenMessages.PostScreenMessage("Power Pack Deactivated", 3, ScreenMessageStyle.UPPER_CENTER);
					}
				}
				else
				{
					powerPackEnabled = false;
					if (FlightGlobals.fetch.activeVessel.isEVA)
					{
						lastEVA = FlightGlobals.fetch.activeVessel;
					}
					else
					{
						lastEVA = null;
					}
				}

				if (lastEVA != null)
				{ 
					foreach(Part part in lastEVA.Parts)
					{
						Debug.Log(part.partName);
						foreach(PartModule module in part.Modules)
						{
							Debug.Log(module.moduleName);
						}

						if (part.Modules.Contains("KerbalEVA"))
						{
							KerbalEVA evaModule = (part.Modules["KerbalEVA"] as KerbalEVA);
							Debug.Log("fFwd: " + evaModule.fFwd.ToString());
							Debug.Log("fRgt: " + evaModule.fRgt.ToString());
							Debug.Log("fUp: " + evaModule.fUp.ToString());
							Debug.Log("Fuel: " + evaModule.Fuel.ToString());
							Debug.Log("FuelCapacity: " + evaModule.FuelCapacity.ToString());
							Debug.Log("PropellantConsumption: " + evaModule.PropellantConsumption.ToString());
							Debug.Log("initialMass: " + evaModule.initialMass.ToString());
							Debug.Log("linPower: " + evaModule.linPower.ToString());
							Debug.Log("rotPower: " + evaModule.rotPower.ToString());

							if (powerPackEnabled)
								evaModule.linPower = 3.0f;
							else
								evaModule.linPower = 0.3f;
						}
					}
				}
			}

		}

	}
}
