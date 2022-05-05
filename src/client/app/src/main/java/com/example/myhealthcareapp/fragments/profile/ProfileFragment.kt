package com.example.myhealthcareapp.fragments.profile

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.TextView
import com.example.myhealthcareapp.MainActivity
import com.example.myhealthcareapp.R
import com.example.myhealthcareapp.cache.SharedPreferencesManager
import com.example.myhealthcareapp.constants.Constant.getDate
import com.example.myhealthcareapp.fragments.BaseFragment
import com.example.myhealthcareapp.fragments.forgotPassword.ForgotPasswordFragment
import com.example.myhealthcareapp.fragments.login.LoginFragment
import kotlinx.android.synthetic.main.activity_main.*
import org.koin.android.ext.android.inject

class ProfileFragment : BaseFragment() {
    private lateinit var clientFullName: TextView
    private lateinit var clientPersonalCode: TextView
    private lateinit var clientEmail: TextView
    private lateinit var clientRegistrationDate: TextView
    private lateinit var resetPassword: TextView
    private lateinit var logoutButton: Button

    private val sharedPreferencesManager: SharedPreferencesManager by inject()

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        val view = inflater.inflate(R.layout.fragment_profile, container, false)

        clientFullName = view.findViewById(R.id.full_name)
        clientPersonalCode = view.findViewById(R.id.personal_code)
        clientEmail = view.findViewById(R.id.email)
        resetPassword = view.findViewById(R.id.click_here_text_view)
        logoutButton = view.findViewById(R.id.logout_button)

        (mActivity as MainActivity).searchIcon.isVisible = false
        (mActivity as MainActivity).profileIcon.isVisible = false
        (mActivity as MainActivity).topAppBar.title = (mActivity).getString(R.string.profile_title)

        return view
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        val client = sharedPreferencesManager.getUser()
        clientFullName.text = client.name
        clientPersonalCode.text = client.address
        clientEmail.text = client.email
        logoutButton.setOnClickListener{
            sharedPreferencesManager.deleteUser()
            (mActivity as MainActivity).topAppBar.visibility = View.GONE
            (mActivity as MainActivity).replaceFragment(LoginFragment(), R.id.fragment_container)
            (mActivity as MainActivity).bottomNavigation.visibility = View.GONE
        }
        resetPassword.setOnClickListener {
            (mActivity as MainActivity).replaceFragment(ForgotPasswordFragment(), R.id.fragment_container, true)
        }
    }
}