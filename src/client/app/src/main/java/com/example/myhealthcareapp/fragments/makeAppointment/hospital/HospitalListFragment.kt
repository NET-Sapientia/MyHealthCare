package com.example.myhealthcareapp.fragments.makeAppointment.hospital

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.SearchView
import android.widget.Toast
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.example.myhealthcareapp.MainActivity
import com.example.myhealthcareapp.R
import com.example.myhealthcareapp.adapters.HospitalRecyclerViewAdapter
import com.example.myhealthcareapp.constants.Constant.HospitalId
import com.example.myhealthcareapp.constants.Constant.HospitalName
import com.example.myhealthcareapp.fragments.BaseFragment
import com.example.myhealthcareapp.fragments.makeAppointment.department.MedicalDepartmentListFragment
import com.example.myhealthcareapp.util.OnItemClickListener
import com.example.myhealthcareapp.model.response.HospitalData
import kotlinx.android.synthetic.main.activity_main.*
import org.koin.androidx.viewmodel.ext.android.viewModel
import java.util.*

class HospitalListFragment : BaseFragment(), OnItemClickListener {
    private lateinit var hospitals: MutableList<HospitalData>
    private lateinit var recyclerview: RecyclerView
    private lateinit var adapter : HospitalRecyclerViewAdapter
    private val viewModel by viewModel<HospitalListViewModel>()

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val view = inflater.inflate(R.layout.fragment_hospital_list, container, false)

        viewModel.uiState.observe(viewLifecycleOwner) { state ->
            when (state) {
                is HospitalListViewModel.UiState.WithHospitals -> {
                    hospitals = state.hospitals.toMutableList()
                    setupUI(view = view)
                }
                is HospitalListViewModel.UiState.Error -> {
                    Toast.makeText(requireActivity(), "Unable to get hospitals", Toast.LENGTH_LONG).show()
                }
                else -> Unit
            }
        }

        return view
    }

    private fun setupUI(view: View){
        (mActivity as MainActivity).topAppBar.title = (mActivity).getString(R.string.select_hospital)
        (mActivity as MainActivity).topAppBar.visibility = View.VISIBLE
        (mActivity as MainActivity).bottomNavigation.visibility = View.VISIBLE
        (mActivity as MainActivity).searchIcon.isVisible = true
        (mActivity as MainActivity).profileIcon.isVisible = true

        recyclerview = view.findViewById(R.id.recycler_view)
        adapter = HospitalRecyclerViewAdapter(
            hospitalList = hospitals,
            listener = this
        )
        recyclerview.adapter = adapter
        recyclerview.layoutManager = LinearLayoutManager(context)
        recyclerview.setHasFixedSize(true)

        (mActivity as MainActivity).searchView.queryHint = (mActivity).getString(R.string.hospital_hint)

        (mActivity as MainActivity).setProfileIconListener()

        (mActivity as MainActivity).searchIcon.setOnMenuItemClickListener{
            (mActivity as MainActivity).searchView.visibility = View.VISIBLE
            (mActivity as MainActivity).topAppBar.visibility = View.GONE
            (mActivity as MainActivity).searchView.isIconified = false

            return@setOnMenuItemClickListener true
        }

        (mActivity as MainActivity).searchView.setOnQueryTextListener(object : SearchView.OnQueryTextListener {
            override fun onQueryTextSubmit(p0: String?): Boolean {
                return true
            }
            override fun onQueryTextChange(query: String?): Boolean {
                filter(query)
                return true
            }
        })

        (mActivity as MainActivity).searchView.setOnCloseListener {
            (mActivity as MainActivity).searchView.visibility = View.GONE
            (mActivity as MainActivity).topAppBar.visibility = View.VISIBLE
            true
        }
    }

    override fun onItemClick(position: Int) {
        val bundle = Bundle()
        bundle.putString(HospitalName, hospitals[position].hospitalName)
        bundle.putInt(HospitalId, hospitals[position].hospitalId)
        val fragment = MedicalDepartmentListFragment()
        fragment.arguments = bundle

        (mActivity as MainActivity).replaceFragment(fragment,R.id.fragment_container, true)
    }

    private fun filter(text: String?) {
        val filteredList: MutableList<HospitalData> = mutableListOf()

        for (item in hospitals) {
            if (text != null) {
                if (item.hospitalName.lowercase(Locale.getDefault()).contains(text.lowercase(Locale.getDefault()))) {

                    filteredList.add(item)
                }
            }
        }
        if (filteredList.isEmpty()) {
            Toast.makeText(context, "No Data Found", Toast.LENGTH_SHORT).show()
        }

        adapter.filterList(filteredList)
    }
}