// Decompiled with JetBrains decompiler
// Type: Terraria.IO.ResourcePack
// Assembly: Terraria, Version=1.4.0.5, Culture=neutral, PublicKeyToken=null
// MVID: 67F9E73E-0A81-4937-A22C-5515CD405A83
// Assembly location: C:\Program Files (x86)\Steam\steamapps\common\Terraria\Terraria.exe

using Ionic.Zip;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using ReLogic.Content;
using ReLogic.Content.Sources;
using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria.GameContent;

namespace Terraria.IO
{
  public class ResourcePack
  {
    private bool _needsReload = true;
    public readonly string FullPath;
    public readonly string FileName;
    private readonly IServiceProvider _services;
    private readonly bool _isCompressed;
    private readonly ZipFile _zipFile;
    private Texture2D _icon;
    private IContentSource _contentSource;
    private const string ICON_FILE_NAME = "icon.png";
    private const string PACK_FILE_NAME = "pack.json";

    public Texture2D Icon
    {
      get
      {
        if (this._icon == null)
          this._icon = this.CreateIcon();
        return this._icon;
      }
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public string Author { get; private set; }

    public ResourcePackVersion Version { get; private set; }

    public bool IsEnabled { get; set; }

    public int SortingOrder { get; set; }

    public ResourcePack(IServiceProvider services, string path)
    {
      if (File.Exists(path))
        this._isCompressed = true;
      else if (!Directory.Exists(path))
        throw new FileNotFoundException("Unable to find file or folder for resource pack at: " + path);
      this.FileName = Path.GetFileName(path);
      this._services = services;
      this.FullPath = path;
      if (this._isCompressed)
        this._zipFile = ZipFile.Read(path);
      this.LoadManifest();
    }

    public void Refresh()
    {
      this._needsReload = true;
    }

    public IContentSource GetContentSource()
    {
      if (this._needsReload)
      {
        this._contentSource = !this._isCompressed ? (IContentSource) new FileSystemContentSource(Path.Combine(this.FullPath, "Content")) : (IContentSource) new ZipContentSource(this.FullPath, "Content");
        this._contentSource.ContentValidator = (IContentValidator) VanillaContentValidator.Instance;
        this._needsReload = false;
      }
      return this._contentSource;
    }

    private Texture2D CreateIcon()
    {
      if (!this.HasFile("icon.png"))
        return ((Asset<Texture2D>) ((IAssetRepository) XnaExtensions.Get<IAssetRepository>(this._services)).Request<Texture2D>("Images/UI/DefaultResourcePackIcon", (AssetRequestMode) 1)).Value;
      using (Stream stream = this.OpenStream("icon.png"))
        return (Texture2D) ((AssetReaderCollection) XnaExtensions.Get<AssetReaderCollection>(this._services)).Read<Texture2D>(stream, ".png");
    }

    private void LoadManifest()
    {
      if (!this.HasFile("pack.json"))
        throw new FileNotFoundException(string.Format("Resource Pack at \"{0}\" must contain a {1} file.", (object) this.FullPath, (object) "pack.json"));
      JObject jobject;
      using (Stream stream = this.OpenStream("pack.json"))
      {
        using (StreamReader streamReader = new StreamReader(stream))
          jobject = JObject.Parse(streamReader.ReadToEnd());
      }
      // TODO not sure what exactly was going on here
      this.Name = jobject.Value<string>("Name");// (string) Extensions.Value<string>(jobject.Value<IEnumerable<JToken>>("Name"));
      this.Description = jobject.Value<string>("Description");// (string) Extensions.Value<string>((IEnumerable<JToken>) jobject.get_Item("Description"));
      this.Author = jobject.Value<string>("Author");// (string) Extensions.Value<string>((IEnumerable<JToken>) jobject.get_Item("Author"));
      this.Version = jobject.Value<ResourcePackVersion>("Version");
    }

    private Stream OpenStream(string fileName)
    {
      if (!this._isCompressed)
        return (Stream) File.OpenRead(Path.Combine(this.FullPath, fileName));
      ZipEntry zipEntry = this._zipFile[fileName];
      MemoryStream memoryStream = new MemoryStream((int) zipEntry.UncompressedSize);
      zipEntry.Extract((Stream) memoryStream);
      memoryStream.Position = 0L;
      return (Stream) memoryStream;
    }

    private bool HasFile(string fileName)
    {
      return !this._isCompressed ? File.Exists(Path.Combine(this.FullPath, fileName)) : this._zipFile.ContainsEntry(fileName);
    }
  }
}
