using UnityEngine;

namespace TakuMenu.Mods
{
    /// <summary>Safety and quality-of-life helpers for your own player.</summary>
    internal static class AdvantageMods
    {
        // ---- 19. Safe Landing: caps how fast you can fall ----
        public static void SafeLanding()
        {
            Rigidbody body = ModUtil.Body;
            if (body == null) return;
            Vector3 v = body.linearVelocity;
            if (v.y < -15f) body.linearVelocity = new Vector3(v.x, -15f, v.z);
        }

        // ---- 20. Auto Checkpoint: saves a checkpoint every 5s while you're standing still ----
        private static float nextCheckpoint;

        public static void AutoCheckpoint()
        {
            GorillaLocomotion.GTPlayer player = GorillaLocomotion.GTPlayer.Instance;
            Rigidbody body = ModUtil.Body;
            if (player == null || body == null || Time.time < nextCheckpoint) return;

            if (body.linearVelocity.magnitude < 1f)
            {
                MovementMods.StoreCheckpoint(player.transform.position);
                nextCheckpoint = Time.time + 5f;
            }
        }

        // ---- 21. Air Jump: tap A in the air for another boost (0.6s cooldown) ----
        private static bool airJumpWasPressed;
        private static float nextAirJump;

        public static void AirJump()
        {
            Rigidbody body = ModUtil.Body;
            ControllerInputPoller input = ModUtil.Input;
            if (body == null || input == null) return;

            bool pressed = input.rightControllerPrimaryButton;
            if (pressed && !airJumpWasPressed && Time.time >= nextAirJump)
            {
                nextAirJump = Time.time + 0.6f;
                Vector3 v = body.linearVelocity;
                body.linearVelocity = new Vector3(v.x, 9f, v.z);
            }
            airJumpWasPressed = pressed;
        }

        // ---- 22. Speed Limiter: caps horizontal speed ----
        public static void SpeedLimiter()
        {
            Rigidbody body = ModUtil.Body;
            if (body == null) return;

            Vector3 v = body.linearVelocity;
            Vector3 flat = new Vector3(v.x, 0f, v.z);
            if (flat.magnitude > 9f)
            {
                flat = flat.normalized * 9f;
                body.linearVelocity = new Vector3(flat.x, v.y, flat.z);
            }
        }

        // ---- 23. Fall Warning ----
        private static float nextWarning;

        public static void FallWarning()
        {
            Rigidbody body = ModUtil.Body;
            if (body == null || Time.time < nextWarning) return;

            if (body.linearVelocity.y < -25f)
            {
                nextWarning = Time.time + 2f;
                ModUtil.Say("FALL", "You're falling fast.");
            }
        }

        // ---- 24. Speed Alert ----
        private static float nextSpeedAlert;

        public static void SpeedAlert()
        {
            Rigidbody body = ModUtil.Body;
            if (body == null || Time.time < nextSpeedAlert) return;

            float speed = body.linearVelocity.magnitude;
            if (speed > 20f)
            {
                nextSpeedAlert = Time.time + 3f;
                ModUtil.Say("SPEED", speed.ToString("0.0") + " m/s");
            }
        }
    }
}
