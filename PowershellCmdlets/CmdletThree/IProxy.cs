namespace CmdletThree
{
    /// <summary>
    /// Interface to decorate proxy classes in which work will be done in other AppDomains.
    /// </summary>
    /// <remarks>
    /// NOTE: For some reason the entire execution sandbox idea falls apart without using this thing.  Not sure why just yet...
    /// </remarks>
    public interface IProxy
    {
        /// <summary>
        /// Do some *real* work
        /// </summary>
        /// <returns>A bit of data to export to the console</returns>
        string DoWork();
    }
}
