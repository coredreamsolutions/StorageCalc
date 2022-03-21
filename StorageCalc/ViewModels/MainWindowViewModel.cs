﻿using System;

namespace StorageCalc.ViewModels
{
    public class MainWindowViewModel
    {
        IMessageBoxHelper messageBox;    

        public MainWindowViewModel(IMessageBoxHelper messageBox)
        {
            this.messageBox = messageBox;
        }
        public (string TotalSize, string FaultTolerance) Calculate(string txtDiskCount, string txtDiskSpace, bool? raid0, bool? raid1, bool? raid5, bool? raid6, bool? raid10)
        {
            try
            {
                var diskCount = Convert.ToInt32(txtDiskCount);
                var diskSpace = Convert.ToDouble(txtDiskSpace);

                var diskspaceInBytes = diskSpace * 1000000000000L;
                var divisor = 1024L * 1024L * 1024L * 1024L;
                var realDiskSpace = (double)diskspaceInBytes / (double)divisor;

                var usableSpace = 0.0d;
                var faultTolerance = string.Empty;

                if (raid0 == true)
                {
                    usableSpace = diskCount * realDiskSpace;
                    faultTolerance = "keine";
                }

                if (raid1 == true)
                {
                    if (diskCount == 2)
                    {
                        usableSpace = realDiskSpace;
                        faultTolerance = "1 Platte";
                    }
                    else
                    {
                        messageBox.Show("Genau 2 Platten benötigt");
                        return default;
                    }
                }

                if (raid5 == true)
                {
                    if (diskCount >= 3)
                    {
                        usableSpace = (diskCount - 1) * realDiskSpace;
                        faultTolerance = "1 Platte";
                    }
                    else
                    {
                        messageBox.Show("Min. 3 Platten notwendig");
                        return default;
                    }
                }

                if (raid6 == true)
                {
                    if (diskCount >= 4)
                    {
                        usableSpace = (diskCount - 2) * realDiskSpace;
                        faultTolerance = "2 Platten";
                    }
                    else
                    {
                        messageBox.Show("Min. 4 Platten notwendig");
                        return default;
                    }
                }

                if (raid10 == true)
                {
                    if (diskCount % 2 == 0 && diskCount >= 4)
                    {
                        usableSpace = (diskCount - 2) * realDiskSpace;
                        faultTolerance = "Min. 1 Platte";
                    }
                    else
                    {
                        messageBox.Show("Min. 4 Platten und gerade Anzahl notwendig");
                        return default; 
                    }
                }

                return(Math.Round(usableSpace, 2) + " TB", faultTolerance);

            }
            catch (FormatException)
            {
                messageBox.Show("Eingabe prüfen, bitte nur Zahlen eingeben");
                return default;
            }
            catch (OverflowException)
            {
                messageBox.Show("Overflow, bitte kleinere Zahlen eingeben");
                return default;
            }
    
        }
    }
}
