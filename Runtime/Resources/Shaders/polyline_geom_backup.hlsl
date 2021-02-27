 [maxvertexcount(6)]
void geom(point v2g i[1], inout TriangleStream<g2f> triangleStream)
{
    uint vid = i[0].vertexID;
    uint vid0 = vid - 1;
    uint vid2 = vid + 1;
    uint vid3 = vid + 2;

    Properties i0 = _Properties[vid0];
    Properties i1 = _Properties[vid];
    Properties i2 = _Properties[vid2];
    Properties i3 = _Properties[vid3];

    if (i2.ID.x != i1.ID.x) return;

    // Looping
    if (i1.ID.z > 0)
    {
        vid0 = vid + i1.ID.z;
        i0 = _Properties[vid0];
    }
    else if (i2.ID.z > 0)
    {
        vid3 = vid - i2.ID.z + 1;
        i3 = _Properties[vid3];
    }

    float3 prev = i0.Position;
    float3 pA = i1.Position;
    float3 pB = i2.Position;
    float3 next = i3.Position;

    float w1 = i1.Width / 2.0;
    float w2 = i2.Width / 2.0;

    float3 prevDir = normalize(pA - prev);
    float3 currentDir = normalize(pB - pA);
    float3 nextDir = normalize(next - pB);

    float3 prevNormal = cross( WorldSpaceViewDir( float4(pA, 1.0) ), prevDir );
    float3 currentNormal = cross( WorldSpaceViewDir( float4((pA + pB) / 2.0, 1.0) ), currentDir );
    float3 nextNormal = cross( WorldSpaceViewDir( float4(pB, 1.0) ), nextDir );
    prevNormal = normalize(prevNormal);
    currentNormal = normalize(currentNormal);
    nextNormal = normalize(nextNormal);

    float3 miterA = normalize(prevNormal + currentNormal);
    float3 miterB = normalize(currentNormal + nextNormal);

    if (i0.ID.x != i1.ID.x) miterA = currentNormal;
    if (i3.ID.x != i2.ID.x) miterB = currentNormal;

    float camDistA = CamDistSampleFactorLine(pA, _WorldSpaceCameraPos);
    float camDistB = CamDistSampleFactorLine(pB, _WorldSpaceCameraPos);
    float camDistAvg = (camDistA + camDistB) / 2.0;

    float widthA = w1 * log(camDistA * rcp(w1) * 0.01);
    float widthB = w2 * log(camDistB * rcp(w2) * 0.01);
    widthA = max(widthA, w1);
    widthB = max(widthB, w2);

    widthA /= dot(miterA, currentNormal);
    widthB /= dot(miterB, currentNormal);

    // widthA = clamp(max(widthA * camDistA, widthA), widthA, widthA * 4);
    // widthB = clamp(max(widthB * camDistB, widthB), widthB, widthB * 4);

    float aWidthDiff = (w1 / widthA);
    float bWidthDiff = (w2 / widthB);

    float4 cp1 = UnityObjectToClipPos(float4(pA + miterA * widthA, 1.0));
    float4 cp2 = UnityObjectToClipPos(float4(pA - miterA * widthA, 1.0));
    float4 cp3 = UnityObjectToClipPos(float4(pB - miterB * widthB, 1.0));
    float4 cp4 = UnityObjectToClipPos(float4(pB + miterB * widthB, 1.0));

    g2f g1 = CreateGeomOutput(cp1, i1.Color, widthA, aWidthDiff, float2(0, 0), camDistA);
    g2f g2 = CreateGeomOutput(cp2, i1.Color, widthA, aWidthDiff, float2(1, 0), camDistA);
    g2f g3 = CreateGeomOutput(cp3, i2.Color, widthB, bWidthDiff, float2(1, 1), camDistB);
    g2f g4 = CreateGeomOutput(cp4, i2.Color, widthB, bWidthDiff, float2(0, 1), camDistB);

    triangleStream.Append(g1);
    triangleStream.Append(g2);
    triangleStream.Append(g3);
    triangleStream.Append(g1);
    triangleStream.Append(g4);
    triangleStream.Append(g3);
    triangleStream.RestartStrip();
}

            [maxvertexcount(6)]
            void geom(point v2g i[1], inout TriangleStream<g2f> triangleStream)
            {
                uint vidA = i[0].vertexID;
                uint vidB = vidA + 1;
                uint vidP = vidA - 1;
                uint vidN = vidA + 2;

                Properties propA = _Properties[vidA];
                Properties propB = _Properties[vidB];
                Properties propP = _Properties[vidP];
                Properties propN = _Properties[vidN];

                if (propB.ID.x != propA.ID.x) return;

                float4 pA = float4(propA.Position, 1.0);
                float4 pB = float4(propB.Position, 1.0);
                float4 pP = float4(propP.Position, 1.0);
                float4 pN = float4(propN.Position, 1.0);

                float4 center = (pA + pB) / 2.0;

                float wA = propA.Width / 2.0;
                float wB = propB.Width / 2.0;

                float4 currDir = float4(normalize(pB.xyz - pA.xyz), 0.0);
                float4 prevDir = float4(normalize(pA.xyz - pP.xyz), 0.0);
                float4 nextDir = float4(normalize(pN.xyz - pB.xyz), 0.0);

                float4 viewDirA = float4(WorldSpaceViewDir(pA), 0.0);
                float4 viewDirB = float4(WorldSpaceViewDir(pB), 0.0);
                float4 viewDirCenter = float4(WorldSpaceViewDir(center), 0.0);

                float4 prevNormal = normalize(float4(cross(viewDirA.xyz, prevDir.xyz), 0.0));
                float4 currNormal = normalize(float4(cross(viewDirCenter.xyz, currDir.xyz), 0.0));
                float4 nextNormal = normalize(float4(cross(viewDirB.xyz, nextDir.xyz), 0.0));

                float4 miterA = float4(normalize(prevNormal.xyz + currNormal.xyz), 0.0);
                float4 miterB = float4(normalize(currNormal.xyz + nextNormal.xyz), 0.0);

                if (propP.ID.x != propA.ID.x) miterA = currNormal;
                if (propN.ID.x != propB.ID.x) miterB = currNormal;

                float camDistA = CamDistSampleFactorLine(pA, _WorldSpaceCameraPos);
                float camDistB = CamDistSampleFactorLine(pB, _WorldSpaceCameraPos);
                float camDistAvg = (camDistA + camDistB) / 2.0;

                float widthA = wA * log(camDistA * rcp(wA) * 0.01);
                float widthB = wB * log(camDistB * rcp(wB) * 0.01);
                widthA = max(widthA, wA);
                widthB = max(widthB, wB);
                widthA /= dot(miterA, currNormal);
                widthB /= dot(miterB, currNormal);

                float aWidthDiff = (wA / widthA);
                float bWidthDiff = (wB / widthB);

                float4 cp1 = UnityObjectToClipPos(pA + miterA * widthA);
                float4 cp2 = UnityObjectToClipPos(pA - miterA * widthA);
                float4 cp3 = UnityObjectToClipPos(pB - miterB * widthB);
                float4 cp4 = UnityObjectToClipPos(pB + miterB * widthB);

                g2f g1 = CreateGeomOutput(cp1, propA.Color, widthA, aWidthDiff, float2(0, 0), camDistA);
                g2f g2 = CreateGeomOutput(cp2, propA.Color, widthA, aWidthDiff, float2(1, 0), camDistA);
                g2f g3 = CreateGeomOutput(cp3, propB.Color, widthB, bWidthDiff, float2(1, 1), camDistB);
                g2f g4 = CreateGeomOutput(cp4, propB.Color, widthB, bWidthDiff, float2(0, 1), camDistB);

                triangleStream.Append(g1);
                triangleStream.Append(g2);
                triangleStream.Append(g3);
                triangleStream.Append(g1);
                triangleStream.Append(g4);
                triangleStream.Append(g3);
                triangleStream.RestartStrip();
            }