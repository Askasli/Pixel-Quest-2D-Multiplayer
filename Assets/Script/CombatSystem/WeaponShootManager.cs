using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Zenject;

public class WeaponShootManager : IWeaponShootManager
{
    private const float ArrowSpawnDelay = 0.4f;
    private const float ShootCooldown = 0.66f;
    private const float StaminaCostPerShot = 0.1f;
    
    private ICombatInput _combatInput;
    private IUltimateTimer _ultimateTimer;
    private IStaminaManager _staminaManager;
    private IAnimatorManager _animatorManager;
    private IUltimateEnable _ultimateEnable;
    
    private bool shootArrowBool;
    private float time;
    private float timeCoolDown = 0.66f;
    private int spawnShoot;
  
    
    [Inject]
    public void Construct(ICombatInput combatInput, IUltimateTimer ultimateTimer, IStaminaManager staminaManager, IAnimatorManager animatorManager, IUltimateEnable ultimateEnable)
    {
        _combatInput = combatInput;
        _ultimateTimer = ultimateTimer;
        _staminaManager = staminaManager;
        _animatorManager = animatorManager;
        _ultimateEnable = ultimateEnable;
    }


    public void BowShoot(Animator bodyAnimator, Animator handAnimator, GameObject bullet, Transform spawnPoint,
        GameObject player)
    {
        _animatorManager.ShootAnimation(bodyAnimator, shootArrowBool);
        _ultimateTimer.UpdateTimer();
        _staminaManager.UpdateStamina();

        if (_combatInput.IsLeftMouseButtonDown())
        {
            if (time <= 0)
            {
                if (_staminaManager.CanShoot() && !_ultimateEnable.CanUltimate())
                {
                    CoroutineRunner.Instance.StartCoroutine(ArrowSpawn(bullet, spawnPoint, player));
                }

                time = ShootCooldown;
            }
        }
        else
        {
            shootArrowBool = _ultimateEnable.CanUltimate();
        }

        time -= 1 * Time.deltaTime;
    }

    IEnumerator ArrowSpawn(GameObject bulletPrefab, Transform spawnPoint, GameObject player)
    {  
        shootArrowBool = true;
        yield return new WaitForSeconds(ArrowSpawnDelay);

        if (shootArrowBool)
        { 
            _staminaManager.UseStamina(StaminaCostPerShot);
            GameObject arrow = PhotonNetwork.Instantiate(bulletPrefab.name, spawnPoint.position, spawnPoint.rotation);
            arrow.layer = player.layer;
            
        }
    }

   
    public void UltimateBowShoot(Animator handAnimator, GameObject bullet, Transform spawnPoint, GameObject player)
    {
        _animatorManager.UltimateShootAnimation(handAnimator, _ultimateEnable.CanUltimate());
      
     
        AnimatorStateInfo stateInfo = handAnimator.GetCurrentAnimatorStateInfo(0);
        Debug.Log(_ultimateTimer.GetTimeRemaining() + " время на ульт");
        
        if (_combatInput.IsUltimateButtonDown() && _ultimateTimer.IsUltimateReady())
        {
            _ultimateTimer.ResetUltimateTimer();
            _ultimateEnable.SetCanUltimate(true);
        }
        
        if ( _ultimateEnable.CanUltimate())
        {
            if (stateInfo.IsName("UltimateShooting") && stateInfo.normalizedTime >= 0.7f)
            {
                Debug.Log("Spawn Ultimate Arrow");
                CoroutineRunner.Instance.StartCoroutine(UltimateShoot(bullet, spawnPoint, player));
            }
            
            if (stateInfo.IsName("UltimateShooting") && stateInfo.normalizedTime >= 1f)
            {
                Debug.Log("UltimateShoot animation clip is over.");
                _ultimateEnable.SetCanUltimate(false);
                spawnShoot = 0;
            }
        }
    }

    IEnumerator UltimateShoot(GameObject bulletPrefab, Transform spawnPoint, GameObject player)
    {
        if (spawnShoot != 1)
        {
            GameObject ultimateArrow =
                PhotonNetwork.Instantiate(bulletPrefab.name, spawnPoint.position, spawnPoint.rotation);
            ultimateArrow.layer = player.layer;
            spawnShoot = 1;
        }

        yield return null;
    }
}