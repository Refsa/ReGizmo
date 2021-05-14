## ReGizmo

**ReGizmo is a gizmo drawing library aiming to give access to gizmos from anywhere.**

### Features:
- Legacy/URP/HDRP Support
- Call from anywhere and it will render to both Scene View and Game View
- Sample-Space AA and Filtering, enjoy smooth lines in the Scene View  
    Only applies to 2D Shapes and Lines
- Draw from using scopes and avoid doing manual translations
- Font rendering, both in glyph and SDF form  
    Comes with tool to generate MSDF/SDF from your own font (see third party section)
- Visualize Raycast, Boxcast, etc.

## Installation
This package is configured as a Unity UPM Package so you can install it through the Package Manager with this URL:  
``` https://github.com/Refsa/ReGizmo.git#v1.0.0 ```

## General Usage
Since ReGizmo is available from anywhere it can be called from Update, OnDrawGizmos, an Editor Window or anywhere else. Just make sure it is called every frame/repaint event since your draw calls dont persist.

```cs
using ReGizmo.Drawing;

public class SomeClass : MonoBehaviour
{
    [SerializeField] Texture2D iconTexture;

    void Update()
    {
        // Position A, Position B, Color, Width
        ReDraw.Line(Vector3.zero, Vector3.one, Color.green, 1f);
        // Position, Direction, Color, Width
        ReDraw.Arrow(Vector3.zero, Vector3.up, Color.green, 1f);

        // Position, Size, Color
        ReDraw.Sphere(Vector3.zero, Vector3.one, Color.red);
        // Texture, Position, Size
        ReDraw.Icon(iconTexture, Vector3.zero, Size.Pixels(32f));

        // Position, Normal, DrawMode, Size
        ReDraw.Circle(Vector3.zero, Vector3.right, DrawMode.AxisAligned, Size.Units(2f));

        using (new TransformScope(transform))
        {
            ReDraw.Cube(Color.red);
        }
    }
}
```


## Available Draw Calls

**Most, if not all, drawing methods have overrides to fit any situation. Dont want to specify color or position? It can do that.**

```cs
// 3D Primitives
ReDraw.Quad(Vector3 position, Quaternion rotation, Vector3 scale, Color color);
ReDraw.Sphere(Vector3 position, Quaternion rotation, Vector3 scale, Color color);
ReDraw.Cube(Vector3 position, Quaternion rotation, Vector3 scale, Color color);
ReDraw.Capsule(Vector3 position, Quaternion rotation, Vector3 scale, Color color);
ReDraw.Pyramid(Vector3 position, Quaternion rotation, Vector3 scale, Color color);
ReDraw.Octahedron(Vector3 position, Quaternion rotation, Vector3 scale, Color color);
ReDraw.Icosahedron(Vector3 position, Quaternion rotation, Vector3 scale, Color color);
ReDraw.Cylinder(Vector3 position, Quaternion rotation, Vector3 scale, Color color);

// Custom Meshes
ReDraw.Mesh(Mesh mesh, Vector3 position, Quaternion rotation, Vector3 scale, Color color);
ReDraw.WireframeMesh(Mesh mesh, Vector3 position, Quaternion rotation, Vector3 scale, Color color);

// Font/Text
ReDraw.TextSDF("someText", ...);
ReDraw.Text("someText", ...);

// Lines
ReDraw.Line(Vector3 p1, Vector3 p2, Color color1, Color color2, float width1, float width2);
ReDraw.Line(Vector3 p1, Vector3 p2, Color color, float width);

ReDraw.Ray(Vector3 origin, Vector3 direction, Color color, float width);
ReDraw.Arrow(Vector3 position, Vector3 direction, float length, ReGizmo.Drawing.Size arrowSize, float lineWidthPixels, Color color);
ReDraw.PolyLine(PolyLine polyLine); // More on this below

// 2D Shapes
ReDraw.Circle(Vector3 position, Vector3 normal, DrawMode drawMode, Size radius, FillMode fillMode, Color color);
ReDraw.Triangle(Vector3 position, Vector3 normal, DrawMode drawMode, Size radius, FillMode fillMode, Color color);

// 3D Physics
ReDraw.Raycast(Vector3 origin, Vector3 direction, float distance = float.MaxValue, int layerMask = ~0);

// These are also available as extension methods to BoxCollider, SphereCollider and CapsuleCollider under ReGizmo.Drawing.Ext
ReDraw.BoxCast(Vector3 center, Vector3 direction, Vector3 halfExtents, Quaternion orientation, 
               float distance = float.MaxValue, int layerMask = ~0);
ReDraw.SphereCast(Vector3 origin, Vector3 direction, float radius, float distance = float.MaxValue, int layerMask = ~0);
ReDraw.CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float distance = float.MaxValue, int layerMask = ~0);

// These will also display text with the number of hits
ReDraw.OverlapBox(Vector3 center, Vector3 halfExtents, Quaternion orientation, int layerMask = ~0);
ReDraw.OverlapSphere(Vector3 origin, float radius, int layerMask = ~0);
ReDraw.OverlapCapsule(Vector3 point1, Vector3 point2, float radius, int layerMask = ~0);

// 2D Physics
ReDraw.Raycast2D(Vector2 origin, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0);

// These are also available as extension methods to BoxCollider2D, SphereCollider2D and CapsuleCollider2D under ReGizmo.Drawing.Ext
ReDraw.BoxCast2D(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0);
ReDraw.CircleCast2D(Vector2 origin, float radius, Vector2 direction, float distance = float.MaxValue, int layerMask = ~0);
ReDraw.CapsuleCast2D(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, Vector2 direction, 
                     float distance = float.MaxValue, int layerMask = ~0);

// These will also display text with the number of hits
ReDraw.OverlapBox2D(Vector2 origin, Vector2 size, float angle, int layerMask = ~0);
ReDraw.OverlapCircle2D(Vector2 origin, float radius, int layerMask = ~0);
ReDraw.OverlapCapsule2D(Vector2 origin, Vector2 size, CapsuleDirection2D capsuleDirection, float angle, int layerMask = ~0);
```

### Poly Lines
Poly Lines works by first pushing points into the PolyLine struct. This is done with the help of the Builder Pattern to also configure other settings for the PolyLine drawing.

```cs
var polyLine = new PolyLine()
    .Point(Vector3.zero)
    .Point(Vector3.one)
    .Point(Vector3.right)
    .ClosedLoop() // If you want the start and end point to connect
    .Draw();
```

You can turn off disposing of the struct if you want to store it as a member variable to avoid recreating it everytime you want to render it.
```cs
PolyLine polyLine = new PolyLine()
    .Point(Vector3.zero)
    .Point(Vector3.one)
    .Point(Vector3.right)
    .DontDispose();
...

polyLine.Draw();

...
// Just make sure to dispose of it when you're done using it
polyLine.Dispose();
```

You can also use it with a using scope that makes sure it automatically calls Draw and disposes itself.

```cs
using (var polyLine = PolyLine.Scope())
{
    polyLine
        .Point(Vector3.zero)
        .Point(Vector3.one);
}
```

### Scopes  
Scopes exists to share data between different draw calls and restoring the original values afterwards.

```cs
ColorScope
PositionScope
ScaleScope
RotationScope
TransformScope

using (new ColorScope(Color.red))
{
    ReDraw.Sphere(...);
    ...
}
```

### Size
Some draw calls takes the Size struct to control the scale of the drawing. You can pick between Pixels, Units or Percent by using the static methods of the same name on the Size struct.

```cs
// Size will be in pixels
Size.Pixels(32f);
// Size will be in world units
Size.Units(2f);
// Size will be a percent of the overall screen resolution
Size.Percent(0.25f);
```

### Fill Mode
Some draw calls takes a FillMode enum, this controls if the shape should be filled or just an outline.

```cs
FillMode.Outline
FillMode.Fill
```

### Draw Mode
Some draw calls takes a DrawMode enum, this controls the alignment/billboarding of the drawing.

```cs
// Shape is aligned with the given normal axis in world space
DrawMode.AxisAligned 

// Shape is billboarding but fixed around the given normal axis
DrawMode.BillboardAligned

// Shape is fully billboarded and will always face the camera
DrawMode.BillboardFree
```

## Third Party Licenses

### msdfgen / msdf-atlas-gen
[msdfgen license](https://github.com/Chlumsky/msdfgen/blob/master/LICENSE.txt)  
[msdf-atlas-gen license](https://github.com/Chlumsky/msdf-atlas-gen/blob/master/LICENSE.txt)

This library makes use of this library to generate MSDF/SDF font atlases.