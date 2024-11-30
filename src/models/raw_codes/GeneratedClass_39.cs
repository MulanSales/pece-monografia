using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rs317.Sharp
{
public class Model : Animable
{
public static void nullLoader()
{
modelHeaders = null;
aBooleanArray1663 = null;
aBooleanArray1664 = null;
anIntArray1665 = null;
anIntArray1666 = null;
anIntArray1667 = null;
anIntArray1668 = null;
anIntArray1669 = null;
anIntArray1670 = null;
anIntArray1671 = null;
anIntArrayArray1672 = null;
anIntArray1673 = null;
anIntArrayArray1674 = null;
anIntArray1675 = null;
anIntArray1676 = null;
anIntArray1677 = null;
SINE = null;
COSINE = null;
HSLtoRGB = null;
modelIntArray4 = null;
}

public static void init(int modelCount, OnDemandFetcher requester)
{
modelHeaders = new ModelHeader[modelCount];
aOnDemandFetcherParent_1662 = requester;
}

public static void loadModelHeader(byte[] modelData, int modelId)
{
if(modelData == null)
{
ModelHeader modelHeader = modelHeaders[modelId] = new ModelHeader();
modelHeader.vertexCount = 0;
modelHeader.triangleCount = 0;
modelHeader.texturedTriangleCount = 0;
}
else
{
Default317Buffer stream = new Default317Buffer(modelData);
stream.position = modelData.Length - 18;
ModelHeader modelHeader = modelHeaders[modelId] = new ModelHeader();
modelHeader.modelData = modelData;
modelHeader.vertexCount = stream.getUnsignedLEShort();
modelHeader.triangleCount = stream.getUnsignedLEShort();
modelHeader.texturedTriangleCount = stream.getUnsignedByte();
int useTextures = stream.getUnsignedByte();
int useTrianglePriority = stream.getUnsignedByte();
int useAlpha = stream.getUnsignedByte();
int useTriangleSkins = stream.getUnsignedByte();
int useVertexSkins = stream.getUnsignedByte();
int dataLengthX = stream.getUnsignedLEShort();
int dataLengthY = stream.getUnsignedLEShort();
int dataLengthZ = stream.getUnsignedLEShort();
int dataLengthTriangle = stream.getUnsignedLEShort();
int offset = 0;
modelHeader.vertexDirectionOffset = offset;
offset += modelHeader.vertexCount;
modelHeader.triangleTypeOffset = offset;
offset += modelHeader.triangleCount;
modelHeader.trianglePriorityOffset = offset;
if(useTrianglePriority == 255)
offset += modelHeader.triangleCount;
else
modelHeader.trianglePriorityOffset = -useTrianglePriority - 1;
modelHeader.triangleSkinOffset = offset;
if(useTriangleSkins == 1)
offset += modelHeader.triangleCount;
else
modelHeader.triangleSkinOffset = -1;
modelHeader.texturePointerOffset = offset;
if(useTextures == 1)
offset += modelHeader.triangleCount;
else
modelHeader.texturePointerOffset = -1;
modelHeader.vertexSkinOffset = offset;
if(useVertexSkins == 1)
offset += modelHeader.vertexCount;
else
modelHeader.vertexSkinOffset = -1;
modelHeader.triangleAlphaOffset = offset;
if(useAlpha == 1)
offset += modelHeader.triangleCount;
else
modelHeader.triangleAlphaOffset = -1;
modelHeader.triangleDataOffset = offset;
offset += dataLengthTriangle;
modelHeader.colourDataOffset = offset;
offset += modelHeader.triangleCount * 2;
modelHeader.texturedTriangleOffset = offset;
offset += modelHeader.texturedTriangleCount * 6;
modelHeader.dataOffsetX = offset;
offset += dataLengthX;
modelHeader.dataOffsetY = offset;
offset += dataLengthY;
modelHeader.dataOffsetZ = offset;
offset += dataLengthZ;
}
}

public static void resetModel(int j)
{
modelHeaders[j] = null;
}

public static Model getModel(int model)
{
if(modelHeaders == null)
return null;
ModelHeader modelHeader = modelHeaders[model];
if(modelHeader == null)
{
aOnDemandFetcherParent_1662.request(model);
return null;
}
else
{
return new Model(model);
}
}

public static bool isCached(int model)
{
if(model >= modelHeaders.Length)
{
string error = $"Cannot check cache for {model} as it exceeds the metadata container's length.";
signlink.reporterror(error);
throw new ArgumentException($"Cannot check cache for {model} as it exceeds the metadata container's length.");
}

if(modelHeaders == null)
return false;
ModelHeader modelHeader = modelHeaders[model];
if(modelHeader == null)
{
aOnDemandFetcherParent_1662.request(model);
return false;
}
else
{
return true;
}
}

public Model()
{
singleTile = false;
}

public Model(int i)
{
singleTile = false;
ModelHeader ModelHeader = modelHeaders[i];
vertexCount = ModelHeader.vertexCount;
triangleCount = ModelHeader.triangleCount;
texturedTriangleCount = ModelHeader.texturedTriangleCount;
verticesX = new int[vertexCount];
verticesY = new int[vertexCount];
verticesZ = new int[vertexCount];
triangleX = new int[triangleCount];
triangleY = new int[triangleCount];
triangleZ = new int[triangleCount];
texturedTrianglePointsX = new int[texturedTriangleCount];
texturedTrianglePointsY = new int[texturedTriangleCount];
texturedTrianglePointsZ = new int[texturedTriangleCount];
if(ModelHeader.vertexSkinOffset >= 0)
vertexSkins = new int[vertexCount];
if(ModelHeader.texturePointerOffset >= 0)
triangleDrawType = new int[triangleCount];
if(ModelHeader.trianglePriorityOffset >= 0)
trianglePriorities = new int[triangleCount];
else
trianglePriority = -ModelHeader.trianglePriorityOffset - 1;
if(ModelHeader.triangleAlphaOffset >= 0)
triangleAlpha = new int[triangleCount];
if(ModelHeader.triangleSkinOffset >= 0)
triangleSkins = new int[triangleCount];
triangleColours = new int[triangleCount];
Default317Buffer vertexDirectionOffsetStream = new Default317Buffer(ModelHeader.modelData);
vertexDirectionOffsetStream.position = ModelHeader.vertexDirectionOffset;
Default317Buffer xDataOffsetStream = new Default317Buffer(ModelHeader.modelData);
xDataOffsetStream.position = ModelHeader.dataOffsetX;
Default317Buffer yDataOffsetStream = new Default317Buffer(ModelHeader.modelData);
yDataOffsetStream.position = ModelHeader.dataOffsetY;
Default317Buffer zDataOffsetStream = new Default317Buffer(ModelHeader.modelData);
zDataOffsetStream.position = ModelHeader.dataOffsetZ;
Default317Buffer vertexSkinOffsetStream = new Default317Buffer(ModelHeader.modelData);
vertexSkinOffsetStream.position = ModelHeader.vertexSkinOffset;
int baseOffsetX = 0;
int baseOffsetY = 0;
int baseOffsetZ = 0;
for(int j1 = 0; j1 < vertexCount; j1++)
{
int k1 = vertexDirectionOffsetStream.getUnsignedByte();
int i2 = 0;
if((k1 & 1) != 0)
i2 = xDataOffsetStream.getSmartA();
int k2 = 0;
if((k1 & 2) != 0)
k2 = yDataOffsetStream.getSmartA();
int i3 = 0;
if((k1 & 4) != 0)
i3 = zDataOffsetStream.getSmartA();
verticesX[j1] = baseOffsetX + i2;
verticesY[j1] = baseOffsetY + k2;
verticesZ[j1] = baseOffsetZ + i3;
baseOffsetX = verticesX[j1];
baseOffsetY = verticesY[j1];
baseOffsetZ = verticesZ[j1];
if(vertexSkins != null)
vertexSkins[j1] = vertexSkinOffsetStream.getUnsignedByte();
}

vertexDirectionOffsetStream.position = ModelHeader.colourDataOffset;
xDataOffsetStream.position = ModelHeader.texturePointerOffset;
yDataOffsetStream.position = ModelHeader.trianglePriorityOffset;
zDataOffsetStream.position = ModelHeader.triangleAlphaOffset;
vertexSkinOffsetStream.position = ModelHeader.triangleSkinOffset;
for(int l1 = 0; l1 < triangleCount; l1++)
{
triangleColours[l1] = vertexDirectionOffsetStream.getUnsignedLEShort();
if(triangleDrawType != null)
triangleDrawType[l1] = xDataOffsetStream.getUnsignedByte();
if(trianglePriorities != null)
trianglePriorities[l1] = yDataOffsetStream.getUnsignedByte();
if(triangleAlpha != null)
triangleAlpha[l1] = zDataOffsetStream.getUnsignedByte();
if(triangleSkins != null)
triangleSkins[l1] = vertexSkinOffsetStream.getUnsignedByte();
}

vertexDirectionOffsetStream.position = ModelHeader.triangleDataOffset;
xDataOffsetStream.position = ModelHeader.triangleTypeOffset;
int trianglePointOffsetX = 0;
int trianglePointOffsetY = 0;
int trianglePointOffsetZ = 0;
int offset = 0;
for(int triangle = 0; triangle < triangleCount; triangle++)
{
int i4 = xDataOffsetStream.getUnsignedByte();
if(i4 == 1)
{
trianglePointOffsetX = vertexDirectionOffsetStream.getSmartA() + offset;
offset = trianglePointOffsetX;
trianglePointOffsetY = vertexDirectionOffsetStream.getSmartA() + offset;
offset = trianglePointOffsetY;
trianglePointOffsetZ = vertexDirectionOffsetStream.getSmartA() + offset;
offset = trianglePointOffsetZ;
triangleX[triangle] = trianglePointOffsetX;
triangleY[triangle] = trianglePointOffsetY;
triangleZ[triangle] = trianglePointOffsetZ;
}
if(i4 == 2)
{
trianglePointOffsetX = trianglePointOffsetX;
trianglePointOffsetY = trianglePointOffsetZ;
trianglePointOffsetZ = vertexDirectionOffsetStream.getSmartA() + offset;
offset = trianglePointOffsetZ;
triangleX[triangle] = trianglePointOffsetX;
triangleY[triangle] = trianglePointOffsetY;
triangleZ[triangle] = trianglePointOffsetZ;
}
if(i4 == 3)
{
trianglePointOffsetX = trianglePointOffsetZ;
trianglePointOffsetY = trianglePointOffsetY;
trianglePointOffsetZ = vertexDirectionOffsetStream.getSmartA() + offset;
offset = trianglePointOffsetZ;
triangleX[triangle] = trianglePointOffsetX;
triangleY[triangle] = trianglePointOffsetY;
triangleZ[triangle] = trianglePointOffsetZ;
}
if(i4 == 4)
{
int k4 = trianglePointOffsetX;
trianglePointOffsetX = trianglePointOffsetY;
trianglePointOffsetY = k4;
trianglePointOffsetZ = vertexDirectionOffsetStream.getSmartA() + offset;
offset = trianglePointOffsetZ;
triangleX[triangle] = trianglePointOffsetX;
triangleY[triangle] = trianglePointOffsetY;
triangleZ[triangle] = trianglePointOffsetZ;
}
}

vertexDirectionOffsetStream.position = ModelHeader.texturedTriangleOffset;
for(int j4 = 0; j4 < texturedTriangleCount; j4++)
{
texturedTrianglePointsX[j4] = vertexDirectionOffsetStream.getUnsignedLEShort();
texturedTrianglePointsY[j4] = vertexDirectionOffsetStream.getUnsignedLEShort();
texturedTrianglePointsZ[j4] = vertexDirectionOffsetStream.getUnsignedLEShort();
}

}

public Model(int i, Model[] aclass30_sub2_sub4_sub6s)
{
singleTile = false;
bool flag = false;
bool flag1 = false;
bool flag2 = false;
bool flag3 = false;
vertexCount = 0;
triangleCount = 0;
texturedTriangleCount = 0;
trianglePriority = -1;
for(int k = 0; k < i; k++)
{
Model model = aclass30_sub2_sub4_sub6s[k];
if(model != null)
{
vertexCount += model.vertexCount;
triangleCount += model.triangleCount;
texturedTriangleCount += model.texturedTriangleCount;
flag |= model.triangleDrawType != null;
if(model.trianglePriorities != null)
{
flag1 = true;
}
else
{
if(trianglePriority == -1)
trianglePriority = model.trianglePriority;
if(trianglePriority != model.trianglePriority)
flag1 = true;
}
flag2 |= model.triangleAlpha != null;
flag3 |= model.triangleSkins != null;
}
}

verticesX = new int[vertexCount];
verticesY = new int[vertexCount];
verticesZ = new int[vertexCount];
vertexSkins = new int[vertexCount];
triangleX = new int[triangleCount];
triangleY = new int[triangleCount];
triangleZ = new int[triangleCount];
texturedTrianglePointsX = new int[texturedTriangleCount];
texturedTrianglePointsY = new int[texturedTriangleCount];
texturedTrianglePointsZ = new int[texturedTriangleCount];
if(flag)
triangleDrawType = new int[triangleCount];
if(flag1)
trianglePriorities = new int[triangleCount];
if(flag2)
triangleAlpha = new int[triangleCount];
if(flag3)
triangleSkins = new int[triangleCount];
triangleColours = new int[triangleCount];
vertexCount = 0;
triangleCount = 0;
texturedTriangleCount = 0;
int l = 0;
for(int i1 = 0; i1 < i; i1++)
{
Model model_1 = aclass30_sub2_sub4_sub6s[i1];
if(model_1 != null)
{
for(int j1 = 0; j1 < model_1.triangleCount; j1++)
{
if(flag)
if(model_1.triangleDrawType == null)
{
triangleDrawType[triangleCount] = 0;
}
else
{
int k1 = model_1.triangleDrawType[j1];
if((k1 & 2) == 2)
k1 += l << 2;
triangleDrawType[triangleCount] = k1;
}
if(flag1)
if(model_1.trianglePriorities == null)
trianglePriorities[triangleCount] = model_1.trianglePriority;
else
trianglePriorities[triangleCount] = model_1.trianglePriorities[j1];
if(flag2)
if(model_1.triangleAlpha == null)
triangleAlpha[triangleCount] = 0;
else
triangleAlpha[triangleCount] = model_1.triangleAlpha[j1];
if(flag3 && model_1.triangleSkins != null)
triangleSkins[triangleCount] = model_1.triangleSkins[j1];
triangleColours[triangleCount] = model_1.triangleColours[j1];
triangleX[triangleCount] = method465(model_1, model_1.triangleX[j1]);
triangleY[triangleCount] = method465(model_1, model_1.triangleY[j1]);
triangleZ[triangleCount] = method465(model_1, model_1.triangleZ[j1]);
triangleCount++;
}

for(int l1 = 0; l1 < model_1.texturedTriangleCount; l1++)
{
texturedTrianglePointsX[texturedTriangleCount] = method465(model_1, model_1.texturedTrianglePointsX[l1]);
texturedTrianglePointsY[texturedTriangleCount] = method465(model_1, model_1.texturedTrianglePointsY[l1]);
texturedTrianglePointsZ[texturedTriangleCount] = method465(model_1, model_1.texturedTrianglePointsZ[l1]);
texturedTriangleCount++;
}

l += model_1.texturedTriangleCount;
}
}

}

public Model(Model[] aclass30_sub2_sub4_sub6s)
{
int i = 2;//was parameter
singleTile = false;
bool flag1 = false;
bool flag2 = false;
bool flag3 = false;
bool flag4 = false;
vertexCount = 0;
triangleCount = 0;
texturedTriangleCount = 0;
trianglePriority = -1;
for(int k = 0; k < i; k++)
{
Model model = aclass30_sub2_sub4_sub6s[k];
if(model != null)
{
vertexCount += model.vertexCount;
triangleCount += model.triangleCount;
texturedTriangleCount += model.texturedTriangleCount;
flag1 |= model.triangleDrawType != null;
if(model.trianglePriorities != null)
{
flag2 = true;
}
else
{
if(trianglePriority == -1)
trianglePriority = model.trianglePriority;
if(trianglePriority != model.trianglePriority)
flag2 = true;
}
flag3 |= model.triangleAlpha != null;
flag4 |= model.triangleColours != null;
}
}

verticesX = new int[vertexCount];
verticesY = new int[vertexCount];
verticesZ = new int[vertexCount];
triangleX = new int[triangleCount];
triangleY = new int[triangleCount];
triangleZ = new int[triangleCount];
anIntArray1634 = new int[triangleCount];
anIntArray1635 = new int[triangleCount];
anIntArray1636 = new int[triangleCount];
texturedTrianglePointsX = new int[texturedTriangleCount];
texturedTrianglePointsY = new int[texturedTriangleCount];
texturedTrianglePointsZ = new int[texturedTriangleCount];
if(flag1)
triangleDrawType = new int[triangleCount];
if(flag2)
trianglePriorities = new int[triangleCount];
if(flag3)
triangleAlpha = new int[triangleCount];
if(flag4)
triangleColours = new int[triangleCount];
vertexCount = 0;
triangleCount = 0;
texturedTriangleCount = 0;
int i1 = 0;
for(int j1 = 0; j1 < i; j1++)
{
Model model_1 = aclass30_sub2_sub4_sub6s[j1];
if(model_1 != null)
{
int k1 = vertexCount;
for(int l1 = 0; l1 < model_1.vertexCount; l1++)
{
verticesX[vertexCount] = model_1.verticesX[l1];
verticesY[vertexCount] = model_1.verticesY[l1];
verticesZ[vertexCount] = model_1.verticesZ[l1];
vertexCount++;
}

for(int i2 = 0; i2 < model_1.triangleCount; i2++)
{
triangleX[triangleCount] = model_1.triangleX[i2] + k1;
triangleY[triangleCount] = model_1.triangleY[i2] + k1;
triangleZ[triangleCount] = model_1.triangleZ[i2] + k1;
anIntArray1634[triangleCount] = model_1.anIntArray1634[i2];
anIntArray1635[triangleCount] = model_1.anIntArray1635[i2];
anIntArray1636[triangleCount] = model_1.anIntArray1636[i2];
if(flag1)
if(model_1.triangleDrawType == null)
{
triangleDrawType[triangleCount] = 0;
}
else
{
int j2 = model_1.triangleDrawType[i2];
if((j2 & 2) == 2)
j2 += i1 << 2;
triangleDrawType[triangleCount] = j2;
}
if(flag2)
if(model_1.trianglePriorities == null)
trianglePriorities[triangleCount] = model_1.trianglePriority;
else
trianglePriorities[triangleCount] = model_1.trianglePriorities[i2];
if(flag3)
if(model_1.triangleAlpha == null)
triangleAlpha[triangleCount] = 0;
else
triangleAlpha[triangleCount] = model_1.triangleAlpha[i2];
if(flag4 && model_1.triangleColours != null)
triangleColours[triangleCount] = model_1.triangleColours[i2];
triangleCount++;
}

for(int k2 = 0; k2 < model_1.texturedTriangleCount; k2++)
{
texturedTrianglePointsX[texturedTriangleCount] = model_1.texturedTrianglePointsX[k2] + k1;
texturedTrianglePointsY[texturedTriangleCount] = model_1.texturedTrianglePointsY[k2] + k1;
texturedTrianglePointsZ[texturedTriangleCount] = model_1.texturedTrianglePointsZ[k2] + k1;
texturedTriangleCount++;
}

i1 += model_1.texturedTriangleCount;
}
}

calculateDiagonals();
}

public Model(bool flag, bool flag1, bool flag2, Model model)
{
singleTile = false;
vertexCount = model.vertexCount;
triangleCount = model.triangleCount;
texturedTriangleCount = model.texturedTriangleCount;
if(flag2)
{
verticesX = model.verticesX;
verticesY = model.verticesY;
verticesZ = model.verticesZ;
}
else
{
verticesX = new int[vertexCount];
verticesY = new int[vertexCount];
verticesZ = new int[vertexCount];
for(int j = 0; j < vertexCount; j++)
{
verticesX[j] = model.verticesX[j];
verticesY[j] = model.verticesY[j];
verticesZ[j] = model.verticesZ[j];
}

}
if(flag)
{
triangleColours = model.triangleColours;
}
else
{
triangleColours = new int[triangleCount];
Array.Copy(model.triangleColours, 0, triangleColours, 0, triangleCount);

}
if(flag1)
{
triangleAlpha = model.triangleAlpha;
}
else
{
triangleAlpha = new int[triangleCount];
if(model.triangleAlpha == null)
{
for(int l = 0; l < triangleCount; l++)
triangleAlpha[l] = 0;

}
else
{
Array.Copy(model.triangleAlpha, 0, triangleAlpha, 0, triangleCount);

}
}
vertexSkins = model.vertexSkins;
triangleSkins = model.triangleSkins;
triangleDrawType = model.triangleDrawType;
triangleX = model.triangleX;
triangleY = model.triangleY;
triangleZ = model.triangleZ;
trianglePriorities = model.trianglePriorities;
trianglePriority = model.trianglePriority;
texturedTrianglePointsX = model.texturedTrianglePointsX;
texturedTrianglePointsY = model.texturedTrianglePointsY;
texturedTrianglePointsZ = model.texturedTrianglePointsZ;
}

public Model(bool flag, bool flag1, Model model)
{
singleTile = false;
vertexCount = model.vertexCount;
triangleCount = model.triangleCount;
texturedTriangleCount = model.texturedTriangleCount;
if(flag)
{
verticesY = new int[vertexCount];
Buffer.BlockCopy(model.verticesY, 0, verticesY, 0, sizeof(int) * vertexCount);

}
else
{
verticesY = model.verticesY;
}
if(flag1)
{
anIntArray1634 = new int[triangleCount];
anIntArray1635 = new int[triangleCount];
anIntArray1636 = new int[triangleCount];
for(int k = 0; k < triangleCount; k++)
{
anIntArray1634[k] = model.anIntArray1634[k];
anIntArray1635[k] = model.anIntArray1635[k];
anIntArray1636[k] = model.anIntArray1636[k];
}

triangleDrawType = new int[triangleCount];
if(model.triangleDrawType == null)
{
for(int l = 0; l < triangleCount; l++)
triangleDrawType[l] = 0;

}
else
{
Buffer.BlockCopy(model.triangleDrawType, 0, triangleDrawType, 0, sizeof(int) * triangleCount);

}
//base.vertexNormals = new VertexNormal[anInt1626];
base.vertexNormals = new VertexNormal[vertexCount];
for(int j1 = 0; j1 < vertexCount; j1++)
{
VertexNormal class33 = base.vertexNormals[j1] = new VertexNormal();
VertexNormal class33_1 = model.vertexNormals[j1];
class33.x = class33_1.x;
class33.y = class33_1.y;
class33.z = class33_1.z;
class33.magnitude = class33_1.magnitude;
}

vertexNormalOffset = model.vertexNormalOffset;
}
else
{
anIntArray1634 = model.anIntArray1634;
anIntArray1635 = model.anIntArray1635;
anIntArray1636 = model.anIntArray1636;
triangleDrawType = model.triangleDrawType;
}
verticesX = model.verticesX;
verticesZ = model.verticesZ;
triangleColours = model.triangleColours;
triangleAlpha = model.triangleAlpha;
trianglePriorities = model.trianglePriorities;
trianglePriority = model.trianglePriority;
triangleX = model.triangleX;
triangleY = model.triangleY;
triangleZ = model.triangleZ;
texturedTrianglePointsX = model.texturedTrianglePointsX;
texturedTrianglePointsY = model.texturedTrianglePointsY;
texturedTrianglePointsZ = model.texturedTrianglePointsZ;
base.modelHeight = model.modelHeight;
maxY = model.maxY;
diagonal2DAboveOrigin = model.diagonal2DAboveOrigin;
anInt1653 = model.anInt1653;
anInt1652 = model.anInt1652;
minX = model.minX;
maxZ = model.maxZ;
minZ = model.minZ;
maxX = model.maxX;
}

//TODO: Refactor https://github.com/HelloKitty/RS317.Sharp/blob/master/src/Rs317.Library.Client/Model.cs#L1923
public void replaceWithModel(Model model, bool flag)
{
vertexCount = model.vertexCount;
triangleCount = model.triangleCount;
texturedTriangleCount = model.texturedTriangleCount;
if(anIntArray1622.Length < vertexCount)
{
anIntArray1622 = new int[vertexCount + 100];
anIntArray1623 = new int[vertexCount + 100];
anIntArray1624 = new int[vertexCount + 100];
}
verticesX = anIntArray1622;
verticesY = anIntArray1623;
verticesZ = anIntArray1624;
for(int k = 0; k < vertexCount; k++)
{
verticesX[k] = model.verticesX[k];
verticesY[k] = model.verticesY[k];
verticesZ[k] = model.verticesZ[k];
}

if(flag)
{
triangleAlpha = model.triangleAlpha;
}
else
{
if(anIntArray1625.Length < triangleCount)
anIntArray1625 = new int[triangleCount + 100];
triangleAlpha = anIntArray1625;
if(model.triangleAlpha == null)
{
for(int l = 0; l < triangleCount; l++)
triangleAlpha[l] = 0;

}
else
{
Array.Copy(model.triangleAlpha, 0, triangleAlpha, 0, triangleCount);

}
}
triangleDrawType = model.triangleDrawType;
triangleColours = model.triangleColours;
trianglePriorities = model.trianglePriorities;
trianglePriority = model.trianglePriority;
triangleSkin = model.triangleSkin;
vertexSkin = model.vertexSkin;
triangleX = model.triangleX;
triangleY = model.triangleY;
triangleZ = model.triangleZ;
anIntArray1634 = model.anIntArray1634;
anIntArray1635 = model.anIntArray1635;
anIntArray1636 = model.anIntArray1636;
texturedTrianglePointsX = model.texturedTrianglePointsX;
texturedTrianglePointsY = model.texturedTrianglePointsY;
texturedTrianglePointsZ = model.texturedTrianglePointsZ;
}

private int method465(Model model, int i)
{
int j = -1;
int k = model.verticesX[i];
int l = model.verticesY[i];
int i1 = model.verticesZ[i];
for(int j1 = 0; j1 < vertexCount; j1++)
{
if(k != verticesX[j1] || l != verticesY[j1] || i1 != verticesZ[j1])
continue;
j = j1;
break;
}

if(j == -1)
{
verticesX[vertexCount] = k;
verticesY[vertexCount] = l;
verticesZ[vertexCount] = i1;
if(model.vertexSkins != null)
vertexSkins[vertexCount] = model.vertexSkins[i];
j = vertexCount++;
}
return j;
}

public void calculateDiagonals()
{
base.modelHeight = 0;
diagonal2DAboveOrigin = 0;
maxY = 0;
for(int i = 0; i < vertexCount; i++)
{
int j = verticesX[i];
int k = verticesY[i];
int l = verticesZ[i];
if(-k > base.modelHeight)
base.modelHeight = -k;
if(k > maxY)
maxY = k;
int i1 = j * j + l * l;
if(i1 > diagonal2DAboveOrigin)
diagonal2DAboveOrigin = i1;
}
diagonal2DAboveOrigin = (int)(Math.Sqrt(diagonal2DAboveOrigin) + 0.98999999999999999D);
anInt1653 = (int)(Math.Sqrt(diagonal2DAboveOrigin * diagonal2DAboveOrigin + base.modelHeight * base.modelHeight) + 0.98999999999999999D);
anInt1652 = anInt1653 + (int)(Math.Sqrt(diagonal2DAboveOrigin * diagonal2DAboveOrigin + maxY * maxY) + 0.98999999999999999D);
}

public void normalise()
{
base.modelHeight = 0;
maxY = 0;
for(int i = 0; i < vertexCount; i++)
{
int j = verticesY[i];
if(-j > base.modelHeight)
base.modelHeight = -j;
if(j > maxY)
maxY = j;
}

anInt1653 = (int)(Math.Sqrt(diagonal2DAboveOrigin * diagonal2DAboveOrigin + base.modelHeight * base.modelHeight) + 0.98999999999999999D);
anInt1652 = anInt1653 + (int)(Math.Sqrt(diagonal2DAboveOrigin * diagonal2DAboveOrigin + maxY * maxY) + 0.98999999999999999D);
}

private void calculateDiagonalsAndBounds()
{
base.modelHeight = 0;
diagonal2DAboveOrigin = 0;
maxY = 0;
minX = 0xf423f;
maxX = unchecked((int)0xfff0bdc1);
maxZ = unchecked((int)0xfffe7961);
minZ = 0x1869f;
for(int j = 0; j < vertexCount; j++)
{
int x = verticesX[j];
int y = verticesY[j];
int z = verticesZ[j];
if(x < minX)
minX = x;
if(x > maxX)
maxX = x;
if(z < minZ)
minZ = z;
if(z > maxZ)
maxZ = z;
if(-y > base.modelHeight)
base.modelHeight = -y;
if(y > maxY)
maxY = y;
int j1 = x * x + z * z;
if(j1 > diagonal2DAboveOrigin)
diagonal2DAboveOrigin = j1;
}

diagonal2DAboveOrigin = (int)Math.Sqrt(diagonal2DAboveOrigin);
anInt1653 = (int)Math.Sqrt(diagonal2DAboveOrigin * diagonal2DAboveOrigin + base.modelHeight * base.modelHeight);
anInt1652 = anInt1653 + (int)Math.Sqrt(diagonal2DAboveOrigin * diagonal2DAboveOrigin + maxY * maxY);
}

public void createBones()
{
if(vertexSkins != null)
{
int[] ai = new int[256];
int j = 0;
for(int l = 0; l < vertexCount; l++)
{
int j1 = vertexSkins[l];
ai[j1]++;
if(j1 > j)
j = j1;
}

vertexSkin = new int[j + 1][];
for(int k1 = 0; k1 <= j; k1++)
{
vertexSkin[k1] = new int[ai[k1]];
ai[k1] = 0;
}

for(int j2 = 0; j2 < vertexCount; j2++)
{
int l2 = vertexSkins[j2];
vertexSkin[l2][ai[l2]++] = j2;
}

vertexSkins = null;
}
if(triangleSkins != null)
{
int[] ai1 = new int[256];
int k = 0;
for(int i1 = 0; i1 < triangleCount; i1++)
{
int l1 = triangleSkins[i1];
ai1[l1]++;
if(l1 > k)
k = l1;
}

triangleSkin = new int[k + 1][];
for(int i2 = 0; i2 <= k; i2++)
{
triangleSkin[i2] = new int[ai1[i2]];
ai1[i2] = 0;
}

for(int k2 = 0; k2 < triangleCount; k2++)
{
int i3 = triangleSkins[k2];
triangleSkin[i3][ai1[i3]++] = k2;
}

triangleSkins = null;
}
}

//TODO: Refactor https://github.com/HelloKitty/RS317.Sharp/blob/master/src/Rs317.Library.Client/Model.cs#L934
public void applyTransformation(int i)
{
if(vertexSkin == null)
return;
if(i == -1)
return;
Animation animationFrame = Animation.forFrameId(i);
if(animationFrame == null)
return;
Skins skins = animationFrame.animationSkins;
vertexModifierX = 0;
vertexModifierY = 0;
vertexModifierZ = 0;
for(int stepId = 0; stepId < animationFrame.frameCount; stepId++)
{
int opcode = animationFrame.opcodeTable[stepId];
transformFrame(skins.opcodes[opcode], skins.skinList[opcode], animationFrame.transformationX[stepId],
animationFrame.transformationY[stepId], animationFrame.transformationZ[stepId]);
}

}

public void mixAnimationFrames(int[] framesFrom2, int frameId2, int frameId1)
{
if(frameId1 == -1)
return;
if(framesFrom2 == null || frameId2 == -1)
{
applyTransformation(frameId1);
return;
}

Animation animationFrame1 = Animation.forFrameId(frameId1);
if(animationFrame1 == null)
return;
Animation animationFrame2 = Animation.forFrameId(frameId2);
if(animationFrame2 == null)
{
applyTransformation(frameId1);
return;
}

Skins skins = animationFrame1.animationSkins;
vertexModifierX = 0;
vertexModifierY = 0;
vertexModifierZ = 0;
int counter = 0;
int frameCount = framesFrom2[counter++];
for(int frame = 0; frame < animationFrame1.frameCount; frame++)
{
int skin;
for(skin = animationFrame1.opcodeTable[frame]; skin > frameCount; frameCount = framesFrom2[counter++])
;
if(skin != frameCount || skins.opcodes[skin] == 0)
transformFrame(skins.opcodes[skin], skins.skinList[skin], animationFrame1.transformationX[frame],
animationFrame1.transformationY[frame], animationFrame1.transformationZ[frame]);
}

vertexModifierX = 0;
vertexModifierY = 0;
vertexModifierZ = 0;
counter = 0;
frameCount = framesFrom2[counter++];
for(int frame = 0; frame < animationFrame2.frameCount; frame++)
{
int skin;
for(skin = animationFrame2.opcodeTable[frame]; skin > frameCount; frameCount = framesFrom2[counter++])
;
if(skin == frameCount || skins.opcodes[skin] == 0)
transformFrame(skins.opcodes[skin], skins.skinList[skin], animationFrame2.transformationX[frame],
animationFrame2.transformationY[frame], animationFrame2.transformationZ[frame]);
}

}

private void transformFrame(int i, int[] ai, int j, int k, int l)
{
int i1 = ai.Length;
if(i == 0)
{
int j1 = 0;
vertexModifierX = 0;
vertexModifierY = 0;
vertexModifierZ = 0;
for(int k2 = 0; k2 < i1; k2++)
{
int l3 = ai[k2];
if(l3 < vertexSkin.Length)
{
int[] ai5 = vertexSkin[l3];
for(int i5 = 0; i5 < ai5.Length; i5++)
{
int j6 = ai5[i5];
vertexModifierX += verticesX[j6];
vertexModifierY += verticesY[j6];
vertexModifierZ += verticesZ[j6];
j1++;
}

}
}

if(j1 > 0)
{
vertexModifierX = vertexModifierX / j1 + j;
vertexModifierY = vertexModifierY / j1 + k;
vertexModifierZ = vertexModifierZ / j1 + l;
return;
}
else
{
vertexModifierX = j;
vertexModifierY = k;
vertexModifierZ = l;
return;
}
}
if(i == 1)
{
for(int k1 = 0; k1 < i1; k1++)
{
int l2 = ai[k1];
if(l2 < vertexSkin.Length)
{
int[] ai1 = vertexSkin[l2];
for(int i4 = 0; i4 < ai1.Length; i4++)
{
int j5 = ai1[i4];
verticesX[j5] += j;
verticesY[j5] += k;
verticesZ[j5] += l;
}

}
}

return;
}
if(i == 2)
{
for(int l1 = 0; l1 < i1; l1++)
{
int i3 = ai[l1];
if(i3 < vertexSkin.Length)
{
int[] ai2 = vertexSkin[i3];
for(int j4 = 0; j4 < ai2.Length; j4++)
{
int k5 = ai2[j4];
verticesX[k5] -= vertexModifierX;
verticesY[k5] -= vertexModifierY;
verticesZ[k5] -= vertexModifierZ;
int k6 = (j & 0xff) * 8;
int l6 = (k & 0xff) * 8;
int i7 = (l & 0xff) * 8;
if(i7 != 0)
{
int j7 = SINE[i7];
int i8 = COSINE[i7];
int l8 = verticesY[k5] * j7 + verticesX[k5] * i8 >> 16;
verticesY[k5] = verticesY[k5] * i8 - verticesX[k5] * j7 >> 16;
verticesX[k5] = l8;
}
if(k6 != 0)
{
int k7 = SINE[k6];
int j8 = COSINE[k6];
int i9 = verticesY[k5] * j8 - verticesZ[k5] * k7 >> 16;
verticesZ[k5] = verticesY[k5] * k7 + verticesZ[k5] * j8 >> 16;
verticesY[k5] = i9;
}
if(l6 != 0)
{
int l7 = SINE[l6];
int k8 = COSINE[l6];
int j9 = verticesZ[k5] * l7 + verticesX[k5] * k8 >> 16;
verticesZ[k5] = verticesZ[k5] * k8 - verticesX[k5] * l7 >> 16;
verticesX[k5] = j9;
}
verticesX[k5] += vertexModifierX;
verticesY[k5] += vertexModifierY;
verticesZ[k5] += vertexModifierZ;
}

}
}

return;
}
if(i == 3)
{
for(int i2 = 0; i2 < i1; i2++)
{
int j3 = ai[i2];
if(j3 < vertexSkin.Length)
{
int[] ai3 = vertexSkin[j3];
for(int k4 = 0; k4 < ai3.Length; k4++)
{
int l5 = ai3[k4];
verticesX[l5] -= vertexModifierX;
verticesY[l5] -= vertexModifierY;
verticesZ[l5] -= vertexModifierZ;
verticesX[l5] = (verticesX[l5] * j) / 128;
verticesY[l5] = (verticesY[l5] * k) / 128;
verticesZ[l5] = (verticesZ[l5] * l) / 128;
verticesX[l5] += vertexModifierX;
verticesY[l5] += vertexModifierY;
verticesZ[l5] += vertexModifierZ;
}

}
}

return;
}
if(i == 5 && triangleSkin != null && triangleAlpha != null)
{
for(int j2 = 0; j2 < i1; j2++)
{
int k3 = ai[j2];
if(k3 < triangleSkin.Length)
{
int[] ai4 = triangleSkin[k3];
for(int l4 = 0; l4 < ai4.Length; l4++)
{
int i6 = ai4[l4];
triangleAlpha[i6] += j * 8;
if(triangleAlpha[i6] < 0)
triangleAlpha[i6] = 0;
if(triangleAlpha[i6] > 255)
triangleAlpha[i6] = 255;
}

}
}

}
}

public void rotate90Degrees()
{
for(int j = 0; j < vertexCount; j++)
{
int k = verticesX[j];
verticesX[j] = verticesZ[j];
verticesZ[j] = -k;
}

}

public void rotateX(int degrees)
{
int k = SINE[degrees];
int l = COSINE[degrees];
for(int i1 = 0; i1 < vertexCount; i1++)
{
int j1 = verticesY[i1] * l - verticesZ[i1] * k >> 16;
verticesZ[i1] = verticesY[i1] * k + verticesZ[i1] * l >> 16;
verticesY[i1] = j1;
}
}

public void translate(int x, int y, int z)
{
for(int i1 = 0; i1 < vertexCount; i1++)
{
verticesX[i1] += x;
verticesY[i1] += y;
verticesZ[i1] += z;
}

}

public void recolour(int targetColour, int replacementColour)
{
for(int k = 0; k < triangleCount; k++)
if(triangleColours[k] == targetColour)
triangleColours[k] = replacementColour;

}

//TODO: Refactor again https://github.com/HelloKitty/RS317.Sharp/blob/master/src/Rs317.Library.Client/Model.cs#L1570
public void mirror()
{
for(int j = 0; j < vertexCount; j++)
verticesZ[j] = -verticesZ[j];

for(int k = 0; k < triangleCount; k++)
{
int l = triangleX[k];
triangleX[k] = triangleZ[k];
triangleZ[k] = l;
}
}

public void scaleT(int x, int z, int y)
{
for(int i1 = 0; i1 < vertexCount; i1++)
{
verticesX[i1] = (verticesX[i1] * x) / 128;
verticesY[i1] = (verticesY[i1] * y) / 128;
verticesZ[i1] = (verticesZ[i1] * z) / 128;
}

}

public void applyLighting(int lightMod, int magnitudeMultiplier, int lightX, int lightY, int lightZ, bool flatShading)
{
int lightMagnitude = (int)Math.Sqrt(lightX * lightX + lightY * lightY + lightZ * lightZ);
int magnitude = magnitudeMultiplier * lightMagnitude >> 8;
if(anIntArray1634 == null)
{
anIntArray1634 = new int[triangleCount];
anIntArray1635 = new int[triangleCount];
anIntArray1636 = new int[triangleCount];
}
if(base.vertexNormals == null)
{
base.vertexNormals = new VertexNormal[vertexCount];
for(int l1 = 0; l1 < vertexCount; l1++)
base.vertexNormals[l1] = new VertexNormal();

}
for(int i2 = 0; i2 < triangleCount; i2++)
{
int _triangleX = triangleX[i2];
int _triangleY = triangleY[i2];
int _triangleZ = triangleZ[i2];
int distanceXXY = verticesX[_triangleY] - verticesX[_triangleX];
int distanceYXY = verticesY[_triangleY] - verticesY[_triangleX];
int distanceZXY = verticesZ[_triangleY] - verticesZ[_triangleX];
int distanceXZX = verticesX[_triangleZ] - verticesX[_triangleX];
int distanceYZX = verticesY[_triangleZ] - verticesY[_triangleX];
int distanceZZX = verticesZ[_triangleZ] - verticesZ[_triangleX];
int normalX = distanceYXY * distanceZZX - distanceYZX * distanceZXY;
int normalY = distanceZXY * distanceXZX - distanceZZX * distanceXXY;
int normalZ;
for(normalZ = distanceXXY * distanceYZX - distanceXZX * distanceYXY; normalX > 8192 || normalY > 8192 || normalZ > 8192 || normalX < -8192 || normalY < -8192 || normalZ < -8192; normalZ >>= 1)
{
normalX >>= 1;
normalY >>= 1;
}

int normalLength = (int)Math.Sqrt(normalX * normalX + normalY * normalY + normalZ * normalZ);
if(normalLength <= 0)
normalLength = 1;
normalX = (normalX * 256) / normalLength;
normalY = (normalY * 256) / normalLength;
normalZ = (normalZ * 256) / normalLength;
if(triangleDrawType == null || (triangleDrawType[i2] & 1) == 0)
{
VertexNormal class33_2 = base.vertexNormals[_triangleX];
class33_2.x += normalX;
class33_2.y += normalY;
class33_2.z += normalZ;
class33_2.magnitude++;
class33_2 = base.vertexNormals[_triangleY];
class33_2.x += normalX;
class33_2.y += normalY;
class33_2.z += normalZ;
class33_2.magnitude++;
class33_2 = base.vertexNormals[_triangleZ];
class33_2.x += normalX;
class33_2.y += normalY;
class33_2.z += normalZ;
class33_2.magnitude++;
}
else
{
int lightness = lightMod + (lightX * normalX + lightY * normalY + lightZ * normalZ) / (magnitude + magnitude / 2);
anIntArray1634[i2] = method481(triangleColours[i2], lightness, triangleDrawType[i2]);
}
}

if(flatShading)
{
handleShading(lightMod, magnitude, lightX, lightY, lightZ);
}
else
{
vertexNormalOffset = new VertexNormal[vertexCount];
for(int k2 = 0; k2 < vertexCount; k2++)
{
VertexNormal class33 = base.vertexNormals[k2];
VertexNormal class33_1 = vertexNormalOffset[k2] = new VertexNormal();
class33_1.x = class33.x;
class33_1.y = class33.y;
class33_1.z = class33.z;
class33_1.magnitude = class33.magnitude;
}

}
if(flatShading)
{
calculateDiagonals();
}
else
{
calculateDiagonalsAndBounds();
}
}

public void handleShading(int intensity, int falloff, int lightX, int lightY, int lightZ)
{
for(int triangle = 0; triangle < triangleCount; triangle++)
{
int x = triangleX[triangle];
int y = triangleY[triangle];
int z = triangleZ[triangle];
if(triangleDrawType == null)
{
int i3 = triangleColours[triangle];
VertexNormal class33 = base.vertexNormals[x];
int k2 = intensity + (lightX * class33.x + lightY * class33.y + lightZ * class33.z) / (falloff * class33.magnitude);
anIntArray1634[triangle] = method481(i3, k2, 0);
class33 = base.vertexNormals[y];
k2 = intensity + (lightX * class33.x + lightY * class33.y + lightZ * class33.z) / (falloff * class33.magnitude);
anIntArray1635[triangle] = method481(i3, k2, 0);
class33 = base.vertexNormals[z];
k2 = intensity + (lightX * class33.x + lightY * class33.y + lightZ * class33.z) / (falloff * class33.magnitude);
anIntArray1636[triangle] = method481(i3, k2, 0);
}
else
if((triangleDrawType[triangle] & 1) == 0)
{
int j3 = triangleColours[triangle];
int k3 = triangleDrawType[triangle];
VertexNormal class33_1 = base.vertexNormals[x];
int l2 = intensity + (lightX * class33_1.x + lightY * class33_1.y + lightZ * class33_1.z) / (falloff * class33_1.magnitude);
anIntArray1634[triangle] = method481(j3, l2, k3);
class33_1 = base.vertexNormals[y];
l2 = intensity + (lightX * class33_1.x + lightY * class33_1.y + lightZ * class33_1.z) / (falloff * class33_1.magnitude);
anIntArray1635[triangle] = method481(j3, l2, k3);
class33_1 = base.vertexNormals[z];
l2 = intensity + (lightX * class33_1.x + lightY * class33_1.y + lightZ * class33_1.z) / (falloff * class33_1.magnitude);
anIntArray1636[triangle] = method481(j3, l2, k3);
}
}

base.vertexNormals = null;
vertexNormalOffset = null;
vertexSkins = null;
triangleSkins = null;
if(triangleDrawType != null)
{
for(int l1 = 0; l1 < triangleCount; l1++)
if((triangleDrawType[l1] & 2) == 2)
return;

}
triangleColours = null;
}

private static int method481(int colour, int lightness, int drawType)
{
if((drawType & 2) == 2)
{
if(lightness < 0)
lightness = 0;
else
if(lightness > 127)
lightness = 127;
lightness = 127 - lightness;
return lightness;
}
lightness = lightness * (colour & 0x7f) >> 7;
if(lightness < 2)
lightness = 2;
else
if(lightness > 126)
lightness = 126;
return (colour & 0xff80) + lightness;
}

//TODO: Refactor https://github.com/HelloKitty/RS317.Sharp/blob/master/src/Rs317.Library.Client/Model.cs#L1854
public void renderSingle(int j, int k, int l, int i1, int j1, int k1)
{
int i = 0; //was a parameter
int l1 = Rasterizer.centreX;
int i2 = Rasterizer.centreY;
int j2 = SINE[i];
int k2 = COSINE[i];
int l2 = SINE[j];
int i3 = COSINE[j];
int j3 = SINE[k];
int k3 = COSINE[k];
int l3 = SINE[l];
int i4 = COSINE[l];
int j4 = j1 * l3 + k1 * i4 >> 16;
for(int k4 = 0; k4 < vertexCount; k4++)
{
int l4 = verticesX[k4];
int i5 = verticesY[k4];
int j5 = verticesZ[k4];
if(k != 0)
{
int k5 = i5 * j3 + l4 * k3 >> 16;
i5 = i5 * k3 - l4 * j3 >> 16;
l4 = k5;
}
if(i != 0)
{
int l5 = i5 * k2 - j5 * j2 >> 16;
j5 = i5 * j2 + j5 * k2 >> 16;
i5 = l5;
}
if(j != 0)
{
int i6 = j5 * l2 + l4 * i3 >> 16;
j5 = j5 * i3 - l4 * l2 >> 16;
l4 = i6;
}
l4 += i1;
i5 += j1;
j5 += k1;
int j6 = i5 * i4 - j5 * l3 >> 16;
j5 = i5 * l3 + j5 * i4 >> 16;
i5 = j6;
anIntArray1667[k4] = j5 - j4;
anIntArray1665[k4] = l1 + (l4 << 9) / j5;
anIntArray1666[k4] = i2 + (i5 << 9) / j5;
if(texturedTriangleCount > 0)
{
anIntArray1668[k4] = l4;
anIntArray1669[k4] = i5;
anIntArray1670[k4] = j5;
}
}

try
{
method483(false, false, 0);
}
catch(Exception _ex)
{
}
}

public override void renderAtPoint(int i, int j, int k, int l, int i1, int j1, int k1,
int l1, int i2)
{
unchecked
{
int j2 = l1 * i1 - j1 * l >> 16;
int k2 = k1 * j + j2 * k >> 16;
int l2 = diagonal2DAboveOrigin * k >> 16;
int i3 = k2 + l2;
if (i3 <= 50 || k2 >= 3500)
return;
int j3 = l1 * l + j1 * i1 >> 16;
int k3 = j3 - diagonal2DAboveOrigin << 9;
if (k3 / i3 >= DrawingArea.viewportCentreX)
return;
int l3 = j3 + diagonal2DAboveOrigin << 9;
if (l3 / i3 <= -DrawingArea.viewportCentreX)
return;
int i4 = k1 * k - j2 * j >> 16;
int j4 = diagonal2DAboveOrigin * j >> 16;
int k4 = i4 + j4 << 9;
if (k4 / i3 <= -DrawingArea.viewportCentreY)
return;
int l4 = j4 + (base.modelHeight * k >> 16);
int i5 = i4 - l4 << 9;
if (i5 / i3 >= DrawingArea.viewportCentreY)
return;
int j5 = l2 + (base.modelHeight * j >> 16);
bool flag = false;
if (k2 - j5 <= 50)
flag = true;
bool flag1 = false;
if (i2 > 0 && abool1684)
{
int k5 = k2 - l2;
if (k5 <= 50)
k5 = 50;
if (j3 > 0)
{
k3 /= i3;
l3 /= k5;
}
else
{
l3 /= i3;
k3 /= k5;
}

if (i4 > 0)
{
i5 /= i3;
k4 /= k5;
}
else
{
k4 /= i3;
i5 /= k5;
}

int i6 = cursorX - Rasterizer.centreX;
int k6 = cursorY - Rasterizer.centreY;
if (i6 > k3 && i6 < l3 && k6 > i5 && k6 < k4)
if (singleTile)
resourceId[resourceCount++] = i2;
else
flag1 = true;
}

int l5 = Rasterizer.centreX;
int j6 = Rasterizer.centreY;
int l6 = 0;
int i7 = 0;
if (i != 0)
{
l6 = SINE[i];
i7 = COSINE[i];
}

for (int j7 = 0; j7 < vertexCount; j7++)
{
int k7 = verticesX[j7];
int l7 = verticesY[j7];
int i8 = verticesZ[j7];
if (i != 0)
{
int j8 = i8 * l6 + k7 * i7 >> 16;
i8 = i8 * i7 - k7 * l6 >> 16;
k7 = j8;
}

k7 += j1;
l7 += k1;
i8 += l1;
int k8 = i8 * l + k7 * i1 >> 16;
i8 = i8 * i1 - k7 * l >> 16;
k7 = k8;
k8 = l7 * k - i8 * j >> 16;
i8 = l7 * j + i8 * k >> 16;
l7 = k8;
anIntArray1667[j7] = i8 - k2;
if (i8 >= 50)
{
anIntArray1665[j7] = l5 + (k7 << 9) / i8;
anIntArray1666[j7] = j6 + (l7 << 9) / i8;
}
else
{
anIntArray1665[j7] = -5000;
flag = true;
}

if (flag || texturedTriangleCount > 0)
{
anIntArray1668[j7] = k7;
anIntArray1669[j7] = l7;
anIntArray1670[j7] = i8;
}
}

try
{
method483(flag, flag1, i2);
}
catch (Exception _ex)
{
}
}
}

private void method483(bool flag, bool flag1, int i)
{
for(int j = 0; j < anInt1652; j++)
anIntArray1671[j] = 0;

for(int k = 0; k < triangleCount; k++)
if(triangleDrawType == null || triangleDrawType[k] != -1)
{
int l = triangleX[k];
int k1 = triangleY[k];
int j2 = triangleZ[k];
int i3 = anIntArray1665[l];
int l3 = anIntArray1665[k1];
int k4 = anIntArray1665[j2];
if(flag && (i3 == -5000 || l3 == -5000 || k4 == -5000))
{
aBooleanArray1664[k] = true;
int j5 = (anIntArray1667[l] + anIntArray1667[k1] + anIntArray1667[j2]) / 3 + anInt1653;
anIntArrayArray1672[j5][anIntArray1671[j5]++] = k;
}
else
{
if(flag1 && method486(cursorX, cursorY, anIntArray1666[l], anIntArray1666[k1], anIntArray1666[j2], i3, l3, k4))
{
resourceId[resourceCount++] = i;
flag1 = false;
}
if((i3 - l3) * (anIntArray1666[j2] - anIntArray1666[k1]) - (anIntArray1666[l] - anIntArray1666[k1]) * (k4 - l3) > 0)
{
aBooleanArray1664[k] = false;
aBooleanArray1663[k] = i3 < 0 || l3 < 0 || k4 < 0 || i3 > DrawingArea.centerX || l3 > DrawingArea.centerX || k4 > DrawingArea.centerX;
int k5 = (anIntArray1667[l] + anIntArray1667[k1] + anIntArray1667[j2]) / 3 + anInt1653;
anIntArrayArray1672[k5][anIntArray1671[k5]++] = k;
}
}
}

if(trianglePriorities == null)
{
for(int i1 = anInt1652 - 1; i1 >= 0; i1--)
{
int l1 = anIntArray1671[i1];
if(l1 > 0)
{
int[] ai = anIntArrayArray1672[i1];
for(int j3 = 0; j3 < l1; j3++)
rasterise(ai[j3]);

}
}

return;
}
for(int j1 = 0; j1 < 12; j1++)
{
anIntArray1673[j1] = 0;
anIntArray1677[j1] = 0;
}

for(int i2 = anInt1652 - 1; i2 >= 0; i2--)
{
int k2 = anIntArray1671[i2];
if(k2 > 0)
{
int[] ai1 = anIntArrayArray1672[i2];
for(int i4 = 0; i4 < k2; i4++)
{
int l4 = ai1[i4];
int l5 = trianglePriorities[l4];
int j6 = anIntArray1673[l5]++;
anIntArrayArray1674[l5][j6] = l4;
if(l5 < 10)
anIntArray1677[l5] += i2;
else
if(l5 == 10)
anIntArray1675[j6] = i2;
else
anIntArray1676[j6] = i2;
}

}
}

int l2 = 0;
if(anIntArray1673[1] > 0 || anIntArray1673[2] > 0)
l2 = (anIntArray1677[1] + anIntArray1677[2]) / (anIntArray1673[1] + anIntArray1673[2]);
int k3 = 0;
if(anIntArray1673[3] > 0 || anIntArray1673[4] > 0)
k3 = (anIntArray1677[3] + anIntArray1677[4]) / (anIntArray1673[3] + anIntArray1673[4]);
int j4 = 0;
if(anIntArray1673[6] > 0 || anIntArray1673[8] > 0)
j4 = (anIntArray1677[6] + anIntArray1677[8]) / (anIntArray1673[6] + anIntArray1673[8]);
int i6 = 0;
int k6 = anIntArray1673[10];
int[] ai2 = anIntArrayArray1674[10];
int[] ai3 = anIntArray1675;
if(i6 == k6)
{
i6 = 0;
k6 = anIntArray1673[11];
ai2 = anIntArrayArray1674[11];
ai3 = anIntArray1676;
}
int i5;
if(i6 < k6)
i5 = ai3[i6];
else
i5 = -1000;
for(int l6 = 0; l6 < 10; l6++)
{
while(l6 == 0 && i5 > l2)
{
rasterise(ai2[i6++]);
if(i6 == k6 && ai2 != anIntArrayArray1674[11])
{
i6 = 0;
k6 = anIntArray1673[11];
ai2 = anIntArrayArray1674[11];
ai3 = anIntArray1676;
}
if(i6 < k6)
i5 = ai3[i6];
else
i5 = -1000;
}
while(l6 == 3 && i5 > k3)
{
rasterise(ai2[i6++]);
if(i6 == k6 && ai2 != anIntArrayArray1674[11])
{
i6 = 0;
k6 = anIntArray1673[11];
ai2 = anIntArrayArray1674[11];
ai3 = anIntArray1676;
}
if(i6 < k6)
i5 = ai3[i6];
else
i5 = -1000;
}
while(l6 == 5 && i5 > j4)
{
rasterise(ai2[i6++]);
if(i6 == k6 && ai2 != anIntArrayArray1674[11])
{
i6 = 0;
k6 = anIntArray1673[11];
ai2 = anIntArrayArray1674[11];
ai3 = anIntArray1676;
}
if(i6 < k6)
i5 = ai3[i6];
else
i5 = -1000;
}
int i7 = anIntArray1673[l6];
int[] ai4 = anIntArrayArray1674[l6];
for(int j7 = 0; j7 < i7; j7++)
rasterise(ai4[j7]);

}

while(i5 != -1000)
{
rasterise(ai2[i6++]);
if(i6 == k6 && ai2 != anIntArrayArray1674[11])
{
i6 = 0;
ai2 = anIntArrayArray1674[11];
k6 = anIntArray1673[11];
ai3 = anIntArray1676;
}
if(i6 < k6)
i5 = ai3[i6];
else
i5 = -1000;
}
}

//TODO: Reactor again https://github.com/HelloKitty/RS317.Sharp/blob/master/src/Rs317.Library.Client/Model.cs#L1655
private void rasterise(int i)
{
if(aBooleanArray1664[i])
{
method485(i);
return;
}
int j = triangleX[i];
int k = triangleY[i];
int l = triangleZ[i];
Rasterizer.restrictEdges = aBooleanArray1663[i];
if(triangleAlpha == null)
Rasterizer.alpha = 0;
else
Rasterizer.alpha = triangleAlpha[i];
int i1;
if(triangleDrawType == null)
i1 = 0;
else
i1 = triangleDrawType[i] & 3;
if(i1 == 0)
{
Rasterizer.drawShadedTriangle(anIntArray1666[j], anIntArray1666[k], anIntArray1666[l], anIntArray1665[j], anIntArray1665[k], anIntArray1665[l], anIntArray1634[i], anIntArray1635[i], anIntArray1636[i]);
return;
}
if(i1 == 1)
{
Rasterizer.drawFlatTriangle(anIntArray1666[j], anIntArray1666[k], anIntArray1666[l], anIntArray1665[j], anIntArray1665[k], anIntArray1665[l], HSLtoRGB[anIntArray1634[i]]);
return;
}
if(i1 == 2)
{
int j1 = triangleDrawType[i] >> 2;
int l1 = texturedTrianglePointsX[j1];
int j2 = texturedTrianglePointsY[j1];
int l2 = texturedTrianglePointsZ[j1];
Rasterizer.drawTexturedTriangle(anIntArray1666[j], anIntArray1666[k], anIntArray1666[l], anIntArray1665[j], anIntArray1665[k], anIntArray1665[l], anIntArray1634[i], anIntArray1635[i], anIntArray1636[i], anIntArray1668[l1], anIntArray1668[j2], anIntArray1668[l2], anIntArray1669[l1], anIntArray1669[j2], anIntArray1669[l2], anIntArray1670[l1], anIntArray1670[j2], anIntArray1670[l2], triangleColours[i]);
return;
}
if(i1 == 3)
{
int k1 = triangleDrawType[i] >> 2;
int i2 = texturedTrianglePointsX[k1];
int k2 = texturedTrianglePointsY[k1];
int i3 = texturedTrianglePointsZ[k1];
Rasterizer.drawTexturedTriangle(anIntArray1666[j], anIntArray1666[k], anIntArray1666[l], anIntArray1665[j], anIntArray1665[k], anIntArray1665[l], anIntArray1634[i], anIntArray1634[i], anIntArray1634[i], anIntArray1668[i2], anIntArray1668[k2], anIntArray1668[i3], anIntArray1669[i2], anIntArray1669[k2], anIntArray1669[i3], anIntArray1670[i2], anIntArray1670[k2], anIntArray1670[i3], triangleColours[i]);
}
}

private void method485(int i)
{
int j = Rasterizer.centreX;
int k = Rasterizer.centreY;
int l = 0;
int x = triangleX[i];
int y = triangleY[i];
int z = triangleZ[i];
int movedX = anIntArray1670[x];
int movedY = anIntArray1670[y];
int movedZ = anIntArray1670[z];
if(movedX >= 50)
{
anIntArray1678[l] = anIntArray1665[x];
anIntArray1679[l] = anIntArray1666[x];
anIntArray1680[l++] = anIntArray1634[i];
}
else
{
int k2 = anIntArray1668[x];
int k3 = anIntArray1669[x];
int k4 = anIntArray1634[i];
if(movedZ >= 50)
{
int k5 = (50 - movedX) * modelIntArray4[movedZ - movedX];
anIntArray1678[l] = j + (k2 + ((anIntArray1668[z] - k2) * k5 >> 16) << 9) / 50;
anIntArray1679[l] = k + (k3 + ((anIntArray1669[z] - k3) * k5 >> 16) << 9) / 50;
anIntArray1680[l++] = k4 + ((anIntArray1636[i] - k4) * k5 >> 16);
}
if(movedY >= 50)
{
int l5 = (50 - movedX) * modelIntArray4[movedY - movedX];
anIntArray1678[l] = j + (k2 + ((anIntArray1668[y] - k2) * l5 >> 16) << 9) / 50;
anIntArray1679[l] = k + (k3 + ((anIntArray1669[y] - k3) * l5 >> 16) << 9) / 50;
anIntArray1680[l++] = k4 + ((anIntArray1635[i] - k4) * l5 >> 16);
}
}
if(movedY >= 50)
{
anIntArray1678[l] = anIntArray1665[y];
anIntArray1679[l] = anIntArray1666[y];
anIntArray1680[l++] = anIntArray1635[i];
}
else
{
int l2 = anIntArray1668[y];
int l3 = anIntArray1669[y];
int l4 = anIntArray1635[i];
if(movedX >= 50)
{
int i6 = (50 - movedY) * modelIntArray4[movedX - movedY];
anIntArray1678[l] = j + (l2 + ((anIntArray1668[x] - l2) * i6 >> 16) << 9) / 50;
anIntArray1679[l] = k + (l3 + ((anIntArray1669[x] - l3) * i6 >> 16) << 9) / 50;
anIntArray1680[l++] = l4 + ((anIntArray1634[i] - l4) * i6 >> 16);
}
if(movedZ >= 50)
{
int j6 = (50 - movedY) * modelIntArray4[movedZ - movedY];
anIntArray1678[l] = j + (l2 + ((anIntArray1668[z] - l2) * j6 >> 16) << 9) / 50;
anIntArray1679[l] = k + (l3 + ((anIntArray1669[z] - l3) * j6 >> 16) << 9) / 50;
anIntArray1680[l++] = l4 + ((anIntArray1636[i] - l4) * j6 >> 16);
}
}
if(movedZ >= 50)
{
anIntArray1678[l] = anIntArray1665[z];
anIntArray1679[l] = anIntArray1666[z];
anIntArray1680[l++] = anIntArray1636[i];
}
else
{
int i3 = anIntArray1668[z];
int i4 = anIntArray1669[z];
int i5 = anIntArray1636[i];
if(movedY >= 50)
{
int k6 = (50 - movedZ) * modelIntArray4[movedY - movedZ];
anIntArray1678[l] = j + (i3 + ((anIntArray1668[y] - i3) * k6 >> 16) << 9) / 50;
anIntArray1679[l] = k + (i4 + ((anIntArray1669[y] - i4) * k6 >> 16) << 9) / 50;
anIntArray1680[l++] = i5 + ((anIntArray1635[i] - i5) * k6 >> 16);
}
if(movedX >= 50)
{
int l6 = (50 - movedZ) * modelIntArray4[movedX - movedZ];
anIntArray1678[l] = j + (i3 + ((anIntArray1668[x] - i3) * l6 >> 16) << 9) / 50;
anIntArray1679[l] = k + (i4 + ((anIntArray1669[x] - i4) * l6 >> 16) << 9) / 50;
anIntArray1680[l++] = i5 + ((anIntArray1634[i] - i5) * l6 >> 16);
}
}
int j3 = anIntArray1678[0];
int j4 = anIntArray1678[1];
int j5 = anIntArray1678[2];
int i7 = anIntArray1679[0];
int j7 = anIntArray1679[1];
int k7 = anIntArray1679[2];
if((j3 - j4) * (k7 - j7) - (i7 - j7) * (j5 - j4) > 0)
{
Rasterizer.restrictEdges = false;
if(l == 3)
{
if(j3 < 0 || j4 < 0 || j5 < 0 || j3 > DrawingArea.centerX || j4 > DrawingArea.centerX || j5 > DrawingArea.centerX)
Rasterizer.restrictEdges = true;
int drawType;
if(triangleDrawType == null)
drawType = 0;
else
drawType = triangleDrawType[i] & 3;
if(drawType == 0)
Rasterizer.drawShadedTriangle(i7, j7, k7, j3, j4, j5, anIntArray1680[0], anIntArray1680[1], anIntArray1680[2]);
else if(drawType == 1)
Rasterizer.drawFlatTriangle(i7, j7, k7, j3, j4, j5, HSLtoRGB[anIntArray1634[i]]);
else if(drawType == 2)
{
int j8 = triangleDrawType[i] >> 2;
int k9 = texturedTrianglePointsX[j8];
int k10 = texturedTrianglePointsY[j8];
int k11 = texturedTrianglePointsZ[j8];
Rasterizer.drawTexturedTriangle(i7, j7, k7, j3, j4, j5, anIntArray1680[0], anIntArray1680[1], anIntArray1680[2], anIntArray1668[k9], anIntArray1668[k10], anIntArray1668[k11], anIntArray1669[k9], anIntArray1669[k10], anIntArray1669[k11], anIntArray1670[k9], anIntArray1670[k10], anIntArray1670[k11], triangleColours[i]);
}
else if(drawType == 3)
{
int k8 = triangleDrawType[i] >> 2;
int l9 = texturedTrianglePointsX[k8];
int l10 = texturedTrianglePointsY[k8];
int l11 = texturedTrianglePointsZ[k8];
Rasterizer.drawTexturedTriangle(i7, j7, k7, j3, j4, j5, anIntArray1634[i], anIntArray1634[i], anIntArray1634[i], anIntArray1668[l9], anIntArray1668[l10], anIntArray1668[l11], anIntArray1669[l9], anIntArray1669[l10], anIntArray1669[l11], anIntArray1670[l9], anIntArray1670[l10], anIntArray1670[l11], triangleColours[i]);
}
}
if(l == 4)
{
if(j3 < 0 || j4 < 0 || j5 < 0 || j3 > DrawingArea.centerX || j4 > DrawingArea.centerX || j5 > DrawingArea.centerX || anIntArray1678[3] < 0 || anIntArray1678[3] > DrawingArea.centerX)
Rasterizer.restrictEdges = true;
int i8;
if(triangleDrawType == null)
i8 = 0;
else
i8 = triangleDrawType[i] & 3;
if(i8 == 0)
{
Rasterizer.drawShadedTriangle(i7, j7, k7, j3, j4, j5, anIntArray1680[0], anIntArray1680[1], anIntArray1680[2]);
Rasterizer.drawShadedTriangle(i7, k7, anIntArray1679[3], j3, j5, anIntArray1678[3], anIntArray1680[0], anIntArray1680[2], anIntArray1680[3]);
return;
}
if(i8 == 1)
{
int l8 = HSLtoRGB[anIntArray1634[i]];
Rasterizer.drawFlatTriangle(i7, j7, k7, j3, j4, j5, l8);
Rasterizer.drawFlatTriangle(i7, k7, anIntArray1679[3], j3, j5, anIntArray1678[3], l8);
return;
}
if(i8 == 2)
{
int i9 = triangleDrawType[i] >> 2;
int i10 = texturedTrianglePointsX[i9];
int i11 = texturedTrianglePointsY[i9];
int i12 = texturedTrianglePointsZ[i9];
Rasterizer.drawTexturedTriangle(i7, j7, k7, j3, j4, j5, anIntArray1680[0], anIntArray1680[1], anIntArray1680[2], anIntArray1668[i10], anIntArray1668[i11], anIntArray1668[i12], anIntArray1669[i10], anIntArray1669[i11], anIntArray1669[i12], anIntArray1670[i10], anIntArray1670[i11], anIntArray1670[i12], triangleColours[i]);
Rasterizer.drawTexturedTriangle(i7, k7, anIntArray1679[3], j3, j5, anIntArray1678[3], anIntArray1680[0], anIntArray1680[2], anIntArray1680[3], anIntArray1668[i10], anIntArray1668[i11], anIntArray1668[i12], anIntArray1669[i10], anIntArray1669[i11], anIntArray1669[i12], anIntArray1670[i10], anIntArray1670[i11], anIntArray1670[i12], triangleColours[i]);
return;
}
if(i8 == 3)
{
int j9 = triangleDrawType[i] >> 2;
int j10 = texturedTrianglePointsX[j9];
int j11 = texturedTrianglePointsY[j9];
int j12 = texturedTrianglePointsZ[j9];
Rasterizer.drawTexturedTriangle(i7, j7, k7, j3, j4, j5, anIntArray1634[i], anIntArray1634[i], anIntArray1634[i], anIntArray1668[j10], anIntArray1668[j11], anIntArray1668[j12], anIntArray1669[j10], anIntArray1669[j11], anIntArray1669[j12], anIntArray1670[j10], anIntArray1670[j11], anIntArray1670[j12], triangleColours[i]);
Rasterizer.drawTexturedTriangle(i7, k7, anIntArray1679[3], j3, j5, anIntArray1678[3], anIntArray1634[i], anIntArray1634[i], anIntArray1634[i], anIntArray1668[j10], anIntArray1668[j11], anIntArray1668[j12], anIntArray1669[j10], anIntArray1669[j11], anIntArray1669[j12], anIntArray1670[j10], anIntArray1670[j11], anIntArray1670[j12], triangleColours[i]);
}
}
}
}

private bool method486(int i, int j, int k, int l, int i1, int j1, int k1,
int l1)
{
if(j < k && j < l && j < i1)
return false;
if(j > k && j > l && j > i1)
return false;
return !(i < j1 && i < k1 && i < l1) && (i <= j1 || i <= k1 || i <= l1);
}

public static Model aModel_1621 = new Model();
private static int[] anIntArray1622 = new int[2000];
private static int[] anIntArray1623 = new int[2000];
private static int[] anIntArray1624 = new int[2000];
private static int[] anIntArray1625 = new int[2000];
public int vertexCount;
public int[] verticesX;
public int[] verticesY;
public int[] verticesZ;
public int triangleCount;
public int[] triangleX;
public int[] triangleY;
public int[] triangleZ;
private int[] anIntArray1634;
private int[] anIntArray1635;
private int[] anIntArray1636;
public int[] triangleDrawType;
private int[] trianglePriorities;
private int[] triangleAlpha;
public int[] triangleColours;
private int trianglePriority;
private int texturedTriangleCount;
private int[] texturedTrianglePointsX;
private int[] texturedTrianglePointsY;
private int[] texturedTrianglePointsZ;
public int minX;
public int maxX;
public int maxZ;
public int minZ;
public int diagonal2DAboveOrigin;
public int maxY;
private int anInt1652;
private int anInt1653;
public int anInt1654;
private int[] vertexSkins;
private int[] triangleSkins;
public int[][] vertexSkin;
public int[][] triangleSkin;
public bool singleTile;
public VertexNormal[] vertexNormalOffset;
private static ModelHeader[] modelHeaders;
private static OnDemandFetcher aOnDemandFetcherParent_1662;
private static bool[] aBooleanArray1663 = new bool[4096];
private static bool[] aBooleanArray1664 = new bool[4096];
private static int[] anIntArray1665 = new int[4096];
private static int[] anIntArray1666 = new int[4096];
private static int[] anIntArray1667 = new int[4096];
private static int[] anIntArray1668 = new int[4096];
private static int[] anIntArray1669 = new int[4096];
private static int[] anIntArray1670 = new int[4096];
private static int[] anIntArray1671 = new int[1500];
private static int[][] anIntArrayArray1672 = new int[1500][];
private static int[] anIntArray1673 = new int[12];
private static int[][] anIntArrayArray1674 = new int[12][];
private static int[] anIntArray1675 = new int[2000];
private static int[] anIntArray1676 = new int[2000];
private static int[] anIntArray1677 = new int[12];
private static int[] anIntArray1678 = new int[10];
private static int[] anIntArray1679 = new int[10];
private static int[] anIntArray1680 = new int[10];
private static int vertexModifierX;
private static int vertexModifierY;
private static int vertexModifierZ;
public static bool abool1684;
public static int cursorX;
public static int cursorY;
public static int resourceCount;
public static int[] resourceId = new int[1000];
public static int[] SINE;
public static int[] COSINE;
private static int[] HSLtoRGB;
private static int[] modelIntArray4;

static Model()
{
SINE = Rasterizer.SINE;
COSINE = Rasterizer.COSINE;
HSLtoRGB = Rasterizer.HSL_TO_RGB;
modelIntArray4 = Rasterizer.anIntArray1469;

for(int i = 0; i < 1500; i++)
anIntArrayArray1672[i] = new int[512];
for(int i = 0; i < 12; i++)
anIntArrayArray1674[i] = new int[2000];
}
}
}
