using UnityEngine;

namespace RRUnity
{
    public class Ripple : MonoBehaviour
    {
        public static class Radius
        {
            public static readonly float Small = 0.16f;
            public static readonly float Medium = 0.32f;
            public static readonly float Large = 0.64f;
            public static readonly float Huge = 1.28f;
        }

        public static class WaveSize
        {
            public static readonly float Small = 0.1f;
            public static readonly float Medium = 0.2f;
            public static readonly float Large = 0.3f;
            public static readonly float Huge = 0.4f;
        }

        private Camera _camera = null;
        private Material _material = null;
        private float _maxRadius = Radius.Huge;
        private bool _movesWithSource = true;
        private Vector3 _startPosition;
        private Timer _timer = null;
        private float _waveSize = WaveSize.Medium;

        public static Ripple Create()
        {
            if (Camera.main)
            {
                return Camera.main.gameObject.AddComponent<Ripple>();
            }

            return null;
        }

        public static Ripple Create(Transform transform, float radius = -1.0f, float waveSize = -1.0f,
            float animationDuration = -1.0f, bool movesWithSource = true)
        {
            if (Camera.main)
            {
                return Camera.main.gameObject.AddComponent<Ripple>()
                    .PlaceAndStart(transform, radius, waveSize, animationDuration, movesWithSource);
            }

            return null;
        }

        public void Awake()
        {
            _camera = Camera.main;
            _material = new Material(Shader.Find("Custom/Ripple"));
            _timer = new Timer(RRSettings.AnimationDuration.Slow);
        }

        public void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            Graphics.Blit(source, destination, _material);
        }

        public Ripple PlaceAndStart(Transform transform, float radius = -1.0f, float waveSize = -1.0f,
            float animationDuration = -1.0f, bool movesWithSource = true)
        {
            return PlaceAndStart(transform.position, radius, waveSize, animationDuration, movesWithSource);
        }

        public Ripple PlaceAndStart(Vector3 position, float radius = -1.0f, float waveSize = -1.0f,
            float animationDuration = -1.0f, bool movesWithSource = true)
        {
            if (animationDuration < 0)
            {
                animationDuration = RRSettings.AnimationDuration.Slow;
            }

            if (radius < 0)
            {
                radius = Radius.Large;
            }

            if (waveSize < 0)
            {
                waveSize = WaveSize.Medium;
            }

            _maxRadius = radius;
            _movesWithSource = movesWithSource;
            _startPosition = position;
            _waveSize = waveSize;

            _material.SetFloat("_MaxRadius", _maxRadius);
            _material.SetFloat("_WaveSize", _waveSize);

            updateCenterPosition();

            _timer.SetTimeout(animationDuration);
            _timer.Reset();

            return this;
        }

        public void Update()
        {
            if (_timer.AdvanceTime(Time.deltaTime))
            {
                Destroy(this);
            }
            else
            {
                float easedStep = Lerp.EaseOut(_timer.progress, EasingType.Cubic);
                _material.SetFloat("_Radius", _maxRadius * easedStep);

                if (_movesWithSource)
                {
                    updateCenterPosition();
                }
            }
        }

        private void updateCenterPosition()
        {
            Vector2 positionOnScreen = _camera.WorldToViewportPoint(_startPosition);
            _material.SetFloat("_CenterX", positionOnScreen.x);
            _material.SetFloat("_CenterY", positionOnScreen.y);
        }
    }
}