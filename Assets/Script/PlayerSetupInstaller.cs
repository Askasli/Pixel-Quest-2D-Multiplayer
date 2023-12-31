using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Photon.Pun;
using ExitGames.Client.Photon;
using UnityEngine.UI;

namespace Common.Infrastructure
{
    public class PlayerSetupInstaller : MonoInstaller
    {
        [SerializeField] private float maxStamina = 1f;
        [SerializeField] private float staminaRegenRate = 0.1f;
        private const int maxHpt = 100;
        private PhotonView photonView;
        
        public override void InstallBindings()
        {
            Container.Bind<PhotonView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IUltimateTimer>().To<UltimateTimer>().AsSingle(); 
            Container.Bind<IMousePosition>().To<MousePosition>().AsSingle();    
            Container.Bind<ICombatInput>().To<CombatInput>().AsSingle();
            Container.Bind<IAnimatorHandler>().To<AnimatorHandler>().AsSingle();
            Container.Bind<IHandAnimator>().To<HandAnimator>().AsSingle();
            Container.Bind<IMoveInput>().To<MoveInput>().AsSingle();   
            Container.Bind<IFlipSprite >().To<FlipSprite>().AsSingle();
            Container.Bind<IMeleeWeaponAttack >().To<MeleeWeaponAttack>().AsSingle();  
            Container.Bind<IStaminaHandler>().To<StaminaHandler>().AsSingle().WithArguments(maxStamina, staminaRegenRate);
            Container.Bind<ICharacterAnimatorRotation >().To<CharacterAnimatorRotation>().AsSingle();   
            Container.Bind<IMoveCharacter>().To<MoveCharacter>().AsSingle();
            Container.Bind<IDash>().To<Dash>().AsSingle();   
            Container.Bind<IMoveDirection>().To<MoveDirection>().AsSingle();  
            Container.Bind<IRotationEnable>().To<RotationEnable>().AsSingle();
            Container.Bind<IWeaponRotationHandler>().To<WeaponRotationHandler>().AsSingle();
            Container.Bind<IWeaponShootHandler>().To<WeaponShootHandler>().AsSingle();
            Container.Bind<IUltimateEnable>().To<UltimateEnable>().AsSingle(); 
            Container.Bind<IGroundChecker>().To<GroundChecker>().AsSingle(); 
            Container.Bind<ICharacterLayerHandler>().To<CharacterLayerHandler>().AsSingle();
            Container.Bind<IPlayerHealth>().To<PlayerHealth>().AsSingle().WithArguments(maxHpt);
        }
    }
}
