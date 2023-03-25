const Vertex door_open_vertices[] = {
  { { -1.000000f, 2.000000f, 0.250000f }, { 0.104462f, 0.815767f } },
  { { -1.000000f, 0.000000f, -0.250000f }, { 0.005595f, 0.095269f } },
  { { -1.000000f, 0.000000f, 0.250000f }, { 0.104461f, 0.095269f } },
  { { -0.750000f, 0.000000f, 0.250000f }, { 0.104461f, 0.005207f } },
  { { -0.750000f, 0.000000f, 0.250000f }, { 0.153894f, 0.095269f } },
  { { -0.750000f, 1.125000f, 0.250000f }, { 0.153895f, 0.500549f } },
  { { -0.530330f, 1.655330f, 0.250000f }, { 0.197331f, 0.691599f } },
  { { 0.000000f, 1.875000f, 0.250000f }, { 0.302195f, 0.770735f } },
  { { -1.000000f, 2.000000f, -0.250000f }, { 0.005596f, 0.815767f } },
  { { 1.000000f, 2.000000f, 0.250000f }, { 0.499928f, 0.815765f } },
  { { -1.000000f, 2.000000f, -0.250000f }, { 0.994261f, 0.815764f } },
  { { -1.000000f, 2.000000f, 0.250000f }, { 0.994261f, 0.995890f } },
  { { 1.000000f, 2.000000f, 0.250000f }, { 0.598794f, 0.995890f } },
  { { -0.750000f, 0.000000f, -0.250000f }, { 0.944827f, 0.095263f } },
  { { -1.000000f, 0.000000f, -0.250000f }, { 0.994261f, 0.095263f } },
  { { -0.750000f, 1.125000f, -0.250000f }, { 0.944828f, 0.500545f } },
  { { -0.750000f, 0.000000f, -0.250000f }, { 0.005595f, 0.005207f } },
  { { 1.000000f, 2.000000f, -0.250000f }, { 0.598794f, 0.815765f } },
  { { 0.000000f, 1.875000f, -0.250000f }, { 0.796528f, 0.770733f } },
  { { -0.530330f, 1.655330f, -0.250000f }, { 0.901392f, 0.691597f } },
  { { 1.000000f, 0.000000f, -0.250000f }, { 0.598794f, 0.095266f } },
  { { 1.000000f, 0.000000f, 0.250000f }, { 0.499927f, 0.095266f } },
  { { 0.750000f, 1.125000f, 0.250000f }, { 0.450494f, 0.500547f } },
  { { 0.750000f, 0.000000f, 0.250000f }, { 0.450494f, 0.095266f } },
  { { 0.750000f, 0.000000f, 0.250000f }, { 0.499927f, 0.005204f } },
  { { 0.530330f, 1.655330f, 0.250000f }, { 0.407058f, 0.691598f } },
  { { 0.750000f, 0.000000f, -0.250000f }, { 0.598794f, 0.005203f } },
  { { 0.750000f, 0.000000f, -0.250000f }, { 0.648227f, 0.095266f } },
  { { 0.750000f, 1.125000f, -0.250000f }, { 0.648227f, 0.500546f } },
  { { 0.530330f, 1.655330f, -0.250000f }, { 0.691664f, 0.691598f } },
  { { -0.750000f, 1.125000f, -0.250000f }, { 0.152467f, 0.826929f } },
  { { -0.750000f, 0.000000f, 0.250000f }, { 0.007579f, 0.991309f } },
  { { -0.750000f, 0.000000f, -0.250000f }, { 0.007579f, 0.826928f } },
  { { -0.530330f, 1.655330f, 0.250000f }, { 0.226396f, 0.991308f } },
  { { -0.750000f, 1.125000f, 0.250000f }, { 0.152467f, 0.991309f } },
  { { -0.530330f, 1.655330f, -0.250000f }, { 0.226396f, 0.826929f } },
  { { 0.000000f, 1.875000f, 0.250000f }, { 0.300324f, 0.991308f } },
  { { 0.530330f, 1.655330f, -0.250000f }, { 0.374252f, 0.826928f } },
  { { 0.530330f, 1.655330f, 0.250000f }, { 0.374252f, 0.991308f } },
  { { 0.750000f, 1.125000f, -0.250000f }, { 0.448180f, 0.826929f } },
  { { 0.000000f, 1.875000f, -0.250000f }, { 0.300324f, 0.826928f } },
  { { 0.750000f, 1.125000f, 0.250000f }, { 0.448180f, 0.991308f } },
  { { 0.750000f, 0.000000f, -0.250000f }, { 0.593068f, 0.826929f } },
  { { 0.750000f, 0.000000f, 0.250000f }, { 0.593068f, 0.991309f } },
};
const uint16_t door_open_indices[] = {
  0, 1, 2, 1, 3, 2, 
  2, 4, 5, 0, 2, 5, 
  0, 6, 7, 0, 8, 1, 
  9, 0, 7, 0, 5, 6, 
  10, 11, 12, 13, 14, 15, 
  14, 10, 15, 1, 16, 3, 
  10, 17, 18, 18, 19, 10, 
  19, 15, 10, 10, 12, 17, 
  20, 9, 21, 22, 23, 21, 
  22, 21, 9, 20, 21, 24, 
  20, 17, 9, 25, 22, 9, 
  7, 25, 9, 20, 24, 26, 
  20, 27, 28, 17, 20, 28, 
  17, 29, 18, 17, 28, 29, 
  30, 31, 32, 30, 33, 34, 
  30, 34, 31, 30, 35, 33, 
  36, 37, 38, 39, 38, 37, 
  36, 40, 37, 35, 40, 36, 
  35, 36, 33, 39, 41, 38, 
  41, 39, 42, 41, 42, 43, 
};
