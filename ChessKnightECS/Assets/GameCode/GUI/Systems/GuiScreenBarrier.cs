using Fwt.Core.Gui;
using Unity.Entities;

namespace Ck.Logic
{
  [UpdateBefore(typeof(GuiWidgetsApi))]
  public class GuiScreenBarrier: BarrierSystem {}
}