using ModdingTools;

[Injector("Player")]
class Example {

    [Inject("Move(float)")]
    public static void ModifyMovement(ref float amount) {
        //Logger.Info("Setting speed to zero");
        //amount = 0;
    }
}