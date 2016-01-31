using DT;
using UnityEngine;
using System.Collections.Generic;

public class FireParticleSystemController : MonoBehaviour {
    // PRAGMA MARK - Internal
    [SerializeField]
    private float _inwardDriftFactor = 0.01f;
    [SerializeField]
    private Vector3 _driftModifier = Vector3.zero;
    private ParticleSystem _particleSystem;
    private ParticleSystem.Particle[] _particles;

    private void Awake() {
        this._particles = new ParticleSystem.Particle[1000];
        this._particleSystem = this.GetComponent<ParticleSystem>();
    }

    private void Update() {
        Vector3 position = this.transform.position;
        int particleCount = this._particleSystem.GetParticles(_particles);
        for (int i = 0; i < particleCount; i++) {
            ParticleSystem.Particle particle = _particles[i];

            Vector3 inwardDrift = this._inwardDriftFactor * (transform.position - particle.position);
            // NOTE (darren): assume that the particle system is facing upwards
            inwardDrift = Vector3.Scale(inwardDrift, _driftModifier);

            particle.velocity += inwardDrift;

            _particles[i] = particle;
        }

        this._particleSystem.SetParticles(_particles, particleCount);
    }
}