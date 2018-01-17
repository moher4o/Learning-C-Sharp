namespace BookTravel.Data
{
    public static class DataConstants
    {
        public const string AdministratorRole = "Administrator";
        public const string ModeratorRole = "Moderator";

        public const string SuccessMessageKey = "SuccessMessage";
        public const string ErrorMessageKey = "ErrorMessage";

        //Admin Data
        public const string AdminUsername = "samsoft@abv.bg";
        public const string AdminFirstName = "Angel";
        public const string AdminLastName = "Vukov";
        public const string AdminPassword = "Moher4o!Vukov";

        public const int TransfersPerPageCount = 20;

        public const string PdfCertificateFormat = @"
                            <h2 style=""text-align:center"">Booking from {1} - {2}</h2>
                            <br />
                            <h5>Transfer N: {0}</h5>
                            <h5>Registration date: {22}</h5>
                            <br />
                            <h4>Subject: {3}</h4>
                            <h4>From: {1}</h4>
                            <h4>Email: {4}</h4>
                            <h4>Phone: {5}</h4>
                            <h4>Transfer Type: {2}</h4>
                            <h4>Going to: {9}</h4>
                            <br />
                            <h5 style=""text-align:center"">--------------------------------------------------------------------------------------------------------------------------------------------------------------------------</h5>
                            <h4 style=""text-align:center"">We are bringing with us</h4>
                            <br />
                            <h4>Number of our Bags: {10}</h4>
                            <h4>Number of our Ski Bags: {11}</h4>
                            <h4>Number of our snowboard Bags: {12}</h4>
                            <br />
                            <h5 style=""text-align:center"">--------------------------------------------------------------------------------------------------------------------------------------------------------------------------</h5>
                            <h4 style=""text-align:center"">Arrival information</h4>
                             <br />
                            <h4>Number of passengers: {6}</h4>
                            <h4>Babies in car seats: {8}</h4>
                            <h4>Arrival: {13}, {14}, {15}, {16}</h4>
                            <h4>Pick us from: {23}</h4>
                            <br />
                            <h5 style=""text-align:center"">--------------------------------------------------------------------------------------------------------------------------------------------------------------------------</h5>
                            <h4 style=""text-align:center"">Departure information</h4>
                            <br />
                            <h4>Return passengers: {7}</h4>
                            <h4>Departure: {17}, {18}, {19}, {20}</h4>
                            <br />
                            <h5 style=""text-align:center"">--------------------------------------------------------------------------------------------------------------------------------------------------------------------------</h5>
                            <h4 style=""text-align:center"">Additional information</h4>
                            <br />
                            <h4>Message body:</h4>
                            <h4>{21}</h4>
                            <br />
                            <br />
                            <br />
                            <h6 style=""text-align:right"">printed by SAMSOFT</h6>
                            ";
    }
}
