using System.Collections.Generic;
using System.Runtime.Serialization;
using VRD.ConsoleTasks.Janitor;

namespace VRD.Janitor
{
    /// <summary>
    /// Represents list of janitor tasks.
    /// </summary>
    [CollectionDataContract]
    [KnownType(typeof(URLDownLoad))]
    [KnownType(typeof(FTPDownload))]

    public sealed class JanitorTaskList : List<IJanitorTask>
    { }
}