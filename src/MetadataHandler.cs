﻿using System;
using System.Collections.Generic;
using System.Linq;
//using System.Data;
using System.Text;
using Landis.Library.Metadata;
using Landis.Core;
using Edu.Wisc.Forest.Flel.Util;
using System.IO;
using Flel = Edu.Wisc.Forest.Flel;

namespace Landis.Extension.Output.AgeReclass
{
    public static class MetadataHandler
    {

        public static ExtensionMetadata Extension { get; set; }

        public static void InitializeMetadata(int Timestep, string MapFileName, IEnumerable<IMapDefinition> mapDefs)
        {

            ScenarioReplicationMetadata scenRep = new ScenarioReplicationMetadata()
            {
                RasterOutCellArea = PlugIn.ModelCore.CellArea,
                TimeMin = PlugIn.ModelCore.StartTime,
                TimeMax = PlugIn.ModelCore.EndTime,
            };

            Extension = new ExtensionMetadata(PlugIn.ModelCore)
            //Extension = new ExtensionMetadata()
            {
                Name = PlugIn.ExtensionName,
                TimeInterval = Timestep,
                ScenarioReplicationMetadata = scenRep
            };

            //---------------------------------------            
            //          map outputs:         
            //---------------------------------------

            //OutputMetadata mapOut_BiomassRemoved = new OutputMetadata()
            //{
            //    Type = OutputType.Map,
            //    Name = "biomass removed",
            //    FilePath = @HarvestMapName,
            //    Map_DataType = MapDataType.Continuous,
            //    Map_Unit = FieldUnits.Mg_ha,
            //    Visualize = true,
            //};
            //Extension.OutputMetadatas.Add(mapOut_BiomassRemoved);

            foreach(IMapDefinition map in mapDefs)
            {
                OutputMetadata mapOut_biomassRemoved = new OutputMetadata()
                {
                    Type = OutputType.Map,
                    Name = map.Name,
                    FilePath = MapFiles.ReplaceTemplateVars(MapFileName, map.Name, PlugIn.ModelCore.CurrentTime),
                    Map_DataType = MapDataType.Continuous,
                    Visualize = true,
                    //Map_Unit = "categorical",
                };
                Extension.OutputMetadatas.Add(mapOut_biomassRemoved);
            }

            //---------------------------------------
            MetadataProvider mp = new MetadataProvider(Extension);
            mp.WriteMetadataToXMLFile("Metadata", Extension.Name, Extension.Name);
        }
        public static void CreateDirectory(string path)
        {
            //Require.ArgumentNotNull(path);
            path = path.Trim(null);
            if (path.Length == 0)
                throw new ArgumentException("path is empty or just whitespace");

            string dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir))
            {
                Flel.Util.Directory.EnsureExists(dir);
            }

            //return new StreamWriter(path);
            return;
        }
    }
}