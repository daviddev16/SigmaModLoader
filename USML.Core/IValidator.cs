namespace USML {

    /// <summary>
    /// Validator provides a <see cref="Validate"/> method that can be used for
    /// <br>validate some condition or verification</br>
    /// </summary>
    ///
    [Documented(true)]
    public interface IValidator {

        /// <summary>
        /// Validate checks all needed conditions to make the inherit class works
        /// </summary>
        ///
        bool Validate();
    }

}
