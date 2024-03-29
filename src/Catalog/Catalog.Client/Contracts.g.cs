//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v13.19.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

#pragma warning disable 108 // Disable "CS0108 '{derivedDto}.ToJson()' hides inherited member '{dtoBase}.ToJson()'. Use the new keyword if hiding was intended."
#pragma warning disable 114 // Disable "CS0114 '{derivedDto}.RaisePropertyChanged(String)' hides inherited member 'dtoBase.RaisePropertyChanged(String)'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword."
#pragma warning disable 472 // Disable "CS0472 The result of the expression is always 'false' since a value of type 'Int32' is never equal to 'null' of type 'Int32?'
#pragma warning disable 612 // Disable "CS0612 '...' is obsolete"
#pragma warning disable 1573 // Disable "CS1573 Parameter '...' has no matching param tag in the XML comment for ...
#pragma warning disable 1591 // Disable "CS1591 Missing XML comment for publicly visible type or member ..."
#pragma warning disable 8073 // Disable "CS8073 The result of the expression is always 'false' since a value of type 'T' is never equal to 'null' of type 'T?'"
#pragma warning disable 3016 // Disable "CS3016 Arrays as attribute arguments is not CLS-compliant"
#pragma warning disable 8603 // Disable "CS8603 Possible null reference return"

namespace Catalog.Client.Contracts
{
    using System = global::System;

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.19.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial interface IPlaylistsClient
    {
        /// <summary>
        /// Get a list of playlists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// <br/>            
        /// <br/>    GET /playlists
        /// </remarks>
        /// <returns>Returns a collection of playlists with pagination details in the 'x-pagination' header</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<PlaylistDetail>> GetPlaylistsAsync(string name, int page, int limit);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// Get a list of playlists
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// <br/>            
        /// <br/>    GET /playlists
        /// </remarks>
        /// <returns>Returns a collection of playlists with pagination details in the 'x-pagination' header</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<System.Collections.ObjectModel.ObservableCollection<PlaylistDetail>> GetPlaylistsAsync(string name, int page, int limit, System.Threading.CancellationToken cancellationToken);

        /// <summary>
        /// Create a new playlist
        /// </summary>
        /// <returns>Returns new playlist</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task CreatePlaylistAsync(PlaylistForCreate playlist);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// Create a new playlist
        /// </summary>
        /// <returns>Returns new playlist</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task CreatePlaylistAsync(PlaylistForCreate playlist, System.Threading.CancellationToken cancellationToken);

        /// <summary>
        /// Get a single playlist based on specified playlist id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// <br/>            
        /// <br/>    GET /playlists/1000
        /// </remarks>
        /// <param name="playlistId">The playlist id</param>
        /// <returns>Returns a single playlist based on specified playlist id</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<PlaylistDetail> GetPlaylistByIdAsync(int playlistId);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// Get a single playlist based on specified playlist id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// <br/>            
        /// <br/>    GET /playlists/1000
        /// </remarks>
        /// <param name="playlistId">The playlist id</param>
        /// <returns>Returns a single playlist based on specified playlist id</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<PlaylistDetail> GetPlaylistByIdAsync(int playlistId, System.Threading.CancellationToken cancellationToken);

        /// <summary>
        /// Update playlist having specified playlist id
        /// </summary>
        /// <returns>New playlist</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task UpdatePlaylistAsync(int playlistId, PlaylistForUpdate playlist);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// Update playlist having specified playlist id
        /// </summary>
        /// <returns>New playlist</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task UpdatePlaylistAsync(int playlistId, PlaylistForUpdate playlist, System.Threading.CancellationToken cancellationToken);

        /// <summary>
        /// Delete playlist by playlist id
        /// </summary>
        /// <param name="playlistId">Playlist id</param>
        /// <returns>No content</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task DeletePlaylistAsync(int playlistId);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// Delete playlist by playlist id
        /// </summary>
        /// <param name="playlistId">Playlist id</param>
        /// <returns>No content</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task DeletePlaylistAsync(int playlistId, System.Threading.CancellationToken cancellationToken);

    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.19.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial interface IPlaylistTracksClient
    {
        /// <summary>
        /// Add a list of tracks to an existing playlist
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// <br/>            
        /// <br/>    PUT /playlists/{playlistId:int}/tracks/1,2,3,4
        /// </remarks>
        /// <param name="playlistId">Playlist identifier</param>
        /// <param name="trackIds">A list of comma separated track ids</param>
        /// <returns>No content</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task AddTracksToPlaylistAsync(int playlistId, System.Collections.Generic.IEnumerable<int> trackIds);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// Add a list of tracks to an existing playlist
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// <br/>            
        /// <br/>    PUT /playlists/{playlistId:int}/tracks/1,2,3,4
        /// </remarks>
        /// <param name="playlistId">Playlist identifier</param>
        /// <param name="trackIds">A list of comma separated track ids</param>
        /// <returns>No content</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task AddTracksToPlaylistAsync(int playlistId, System.Collections.Generic.IEnumerable<int> trackIds, System.Threading.CancellationToken cancellationToken);

        /// <summary>
        /// Delete a list of tracks from an existing playlist
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// <br/>            
        /// <br/>    DELETE /playlists/{playlistId:int}/tracks/1,2,3,4
        /// </remarks>
        /// <param name="playlistId">Playlist identifier</param>
        /// <param name="trackIds">A list of comma separated track ids</param>
        /// <returns>No content</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task DeleteTracksFromPlaylistAsync(int playlistId, System.Collections.Generic.IEnumerable<int> trackIds);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// Delete a list of tracks from an existing playlist
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// <br/>            
        /// <br/>    DELETE /playlists/{playlistId:int}/tracks/1,2,3,4
        /// </remarks>
        /// <param name="playlistId">Playlist identifier</param>
        /// <param name="trackIds">A list of comma separated track ids</param>
        /// <returns>No content</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task DeleteTracksFromPlaylistAsync(int playlistId, System.Collections.Generic.IEnumerable<int> trackIds, System.Threading.CancellationToken cancellationToken);

    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.19.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial interface ITracksClient
    {
        /// <summary>
        /// Get a list of tracks
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// <br/>            
        /// <br/>    GET /tracks
        /// </remarks>
        /// <returns>Returns a collection of tracks with pagination details in the 'x-pagination' header</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task GetTracksAsync(string name, string composer, string genre, string album, string artist, string media_type, string price_from, string price_to, int page, int limit);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <summary>
        /// Get a list of tracks
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// <br/>            
        /// <br/>    GET /tracks
        /// </remarks>
        /// <returns>Returns a collection of tracks with pagination details in the 'x-pagination' header</returns>
        /// <exception cref="SwaggerException">A server side error occurred.</exception>
        System.Threading.Tasks.Task GetTracksAsync(string name, string composer, string genre, string album, string artist, string media_type, string price_from, string price_to, int page, int limit, System.Threading.CancellationToken cancellationToken);

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.19.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class PlaylistDetail
    {
        [Newtonsoft.Json.JsonProperty("id", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Id { get; set; }

        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("trackCount", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int TrackCount { get; set; }

        public string ToJson()
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());

        }
        public static PlaylistDetail FromJson(string data)
        {

            return Newtonsoft.Json.JsonConvert.DeserializeObject<PlaylistDetail>(data, new Newtonsoft.Json.JsonSerializerSettings());

        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.19.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class ProblemDetails
    {
        [Newtonsoft.Json.JsonProperty("type", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Type { get; set; }

        [Newtonsoft.Json.JsonProperty("title", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Title { get; set; }

        [Newtonsoft.Json.JsonProperty("status", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int? Status { get; set; }

        [Newtonsoft.Json.JsonProperty("detail", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Detail { get; set; }

        [Newtonsoft.Json.JsonProperty("instance", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Instance { get; set; }

        [Newtonsoft.Json.JsonProperty("extensions", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.Generic.Dictionary<string, object> Extensions { get; set; }

        private System.Collections.Generic.IDictionary<string, object> _additionalProperties;

        [Newtonsoft.Json.JsonExtensionData]
        public System.Collections.Generic.IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties ?? (_additionalProperties = new System.Collections.Generic.Dictionary<string, object>()); }
            set { _additionalProperties = value; }
        }

        public string ToJson()
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());

        }
        public static ProblemDetails FromJson(string data)
        {

            return Newtonsoft.Json.JsonConvert.DeserializeObject<ProblemDetails>(data, new Newtonsoft.Json.JsonSerializerSettings());

        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.19.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class PlaylistForCreate
    {
        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("trackIds", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.ObjectModel.ObservableCollection<int> TrackIds { get; set; }

        public string ToJson()
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());

        }
        public static PlaylistForCreate FromJson(string data)
        {

            return Newtonsoft.Json.JsonConvert.DeserializeObject<PlaylistForCreate>(data, new Newtonsoft.Json.JsonSerializerSettings());

        }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.19.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class PlaylistForUpdate
    {
        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Name { get; set; }

        [Newtonsoft.Json.JsonProperty("trackIds", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public System.Collections.ObjectModel.ObservableCollection<int> TrackIds { get; set; }

        public string ToJson()
        {

            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new Newtonsoft.Json.JsonSerializerSettings());

        }
        public static PlaylistForUpdate FromJson(string data)
        {

            return Newtonsoft.Json.JsonConvert.DeserializeObject<PlaylistForUpdate>(data, new Newtonsoft.Json.JsonSerializerSettings());

        }

    }



    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.19.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class SwaggerException : System.Exception
    {
        public int StatusCode { get; private set; }

        public string Response { get; private set; }

        public System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

        public SwaggerException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Exception innerException)
            : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + ((response == null) ? "(null)" : response.Substring(0, response.Length >= 512 ? 512 : response.Length)), innerException)
        {
            StatusCode = statusCode;
            Response = response;
            Headers = headers;
        }

        public override string ToString()
        {
            return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.19.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))")]
    public partial class SwaggerException<TResult> : SwaggerException
    {
        public TResult Result { get; private set; }

        public SwaggerException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result, System.Exception innerException)
            : base(message, statusCode, response, headers, innerException)
        {
            Result = result;
        }
    }

}

#pragma warning restore  108
#pragma warning restore  114
#pragma warning restore  472
#pragma warning restore  612
#pragma warning restore 1573
#pragma warning restore 1591
#pragma warning restore 8073
#pragma warning restore 3016
#pragma warning restore 8603