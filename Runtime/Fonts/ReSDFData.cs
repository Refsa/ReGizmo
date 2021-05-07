using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReGizmo.Core.Fonts
{
    public class ReSDFData : ScriptableObject
    {
        [SerializeField] Texture2D image;
        [SerializeField] List<Texture2D> mipmaps;
        [SerializeField] MSDF.Font font;

        Texture2D runtimeTexture;

        public MSDF.Font Font => font;

        public void Setup(Texture2D image, string jsonInfo)
        {
            if (mipmaps == null) mipmaps = new List<Texture2D>();

            this.image = image;
            ParseJson(jsonInfo);
        }

        public void SetMipTexture(int mip, Texture2D texture)
        {
            mipmaps[mip] = texture;
        }

        public void AddMipTexture(Texture2D texture)
        {
            mipmaps.Add(texture);
        }

        public void ClearMips()
        {
            mipmaps.Clear();
        }

        void ParseJson(string jsonInfo)
        {
            font = JsonUtility.FromJson<MSDF.Font>(jsonInfo);
            font.glyphs = font.glyphs.OrderBy(e => e.unicode).ToArray();
        }

        public bool TryGetGlyph(int unicode, out MSDF.Glyph glyph)
        {
            glyph = font.glyphs.FirstOrDefault(e => e.unicode == unicode);
            return glyph != null;
        }

        public Texture2D GetTexture()
        {
            runtimeTexture = new Texture2D(image.width, image.height, TextureFormat.RGBA32, mipmaps.Count + 1, true);
            runtimeTexture.SetPixels(image.GetPixels(), 0);
            for (int i = 0; i < mipmaps.Count; i++)
            {
                runtimeTexture.SetPixels(mipmaps[i].GetPixels(), i + 1);
            }
            runtimeTexture.Apply(true, true);

            return runtimeTexture;
        }
    }

    namespace MSDF
    {
        [System.Serializable]
        public class Font
        {
            public Atlas atlas;
            public Metrics metrics;
            public Glyph[] glyphs;
        }

        [System.Serializable]
        public class Atlas
        {
            public string type;
            public int distanceRange;
            public float size;
            public int width;
            public int height;
            public string yOrigin;
        }

        [System.Serializable]
        public class Metrics
        {
            public float lineHeight;
            public float ascender;
            public float descender;
            public float underlineY;
            public float underlineThickness;
        }

        [System.Serializable]
        public class Glyph
        {
            public int unicode;
            public float advance;
            public Bounds planeBounds;
            public Bounds atlasBounds;
        }

        [System.Serializable]
        public class Bounds
        {
            public float left;
            public float bottom;
            public float right;
            public float top;
        }
    }
}