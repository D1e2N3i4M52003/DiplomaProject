using Hangfire.Annotations;

namespace PresentationLayer.Helpers
{
    public static class Ensure
    {
        /// <summary>
        /// Ensures that the specified argument is not null.
        /// </summary>
        /// <param name="argumentName">Name of the argument.</param>
        /// <param name="argument">The argument.</param>
        
        public static void ArgumentNotNull(object argument, [InvokerParameterName] string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}
